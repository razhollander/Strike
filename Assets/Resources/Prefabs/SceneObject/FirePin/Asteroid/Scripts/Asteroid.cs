using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Asteroid : PooledMonobehaviour, IUpdatable
{
    const string VORONOI_COLOR = "_voronoiColor";
    const string FRENSEL_COLOR = "_frenselColor";

    [SerializeField] float time = 4;
    [SerializeField] float jumpPower = 5;
    [SerializeField] float hitRadius = 3;
    [SerializeField] float rotationSpeed=1;
    [SerializeField] float scaleSpeed;
    [SerializeField] Transform asteroidObject;
    [SerializeField] ParticleSystem shadowHitPoint;
    [SerializeField] float shadowShakeStrength = 1;
    [SerializeField] int shadowShakeVabration = 10;
    [SerializeField] ParticleSystem hitExplosionParticles;
    [SerializeField] ParticleSystem fireTrailParticles;
    [SerializeField] Renderer thisRenderer;

    private Tween rotationTweener;
    private Vector3 Scale;
    private Vector3 startScale;
    Color asteroidVoronoiColor;
    Color asteroidFrenselColor;
    Material asteroidMaterial;
    Vector3 prevPos;

    public bool UpdateWhenDisabled => false;

    public bool IsEnabled => enabled && gameObject.activeInHierarchy;

    private void Awake()
    {
        UpdateManager.AddItem(this);
        Scale = transform.localScale;
        asteroidMaterial = thisRenderer.material;
        asteroidVoronoiColor = asteroidMaterial.GetColor(VORONOI_COLOR);
        asteroidFrenselColor = asteroidMaterial.GetColor(FRENSEL_COLOR);
    }
    private void OnEnable()
    {
        prevPos = transform.position;
        thisRenderer.enabled = true;
        asteroidMaterial.SetColor(VORONOI_COLOR, asteroidVoronoiColor);
        asteroidMaterial.SetColor(FRENSEL_COLOR, asteroidFrenselColor);
    }
    public void Lounch(Vector3 landPosition, Vector3 startPos)
    {
        Sequence animationSeq = DOTween.Sequence();
        animationSeq.PrependInterval(time / 2);
        animationSeq.Append(shadowHitPoint.transform.DOShakePosition(time/2, shadowShakeStrength, shadowShakeVabration, 90, false, false).SetEase(Ease.InExpo));
        //ShadowPoint
        shadowHitPoint.transform.SetParent(null);
        shadowHitPoint.transform.position = landPosition+Vector3.up*0.05f;
        shadowHitPoint.Play();
        //rotation
        transform.LookAt(new Vector3(landPosition.x,transform.position.y,landPosition.z));
        //Vector3 rotationVec = new Vector3(100*rotationSpeed,0, 0);
        transform.localScale = startScale;
        transform.DOScale(Scale, scaleSpeed);
        //rotationTweener = astroidObject.DORotate(rotationVec, time*1.2f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
        //Lounch
        transform.position = startPos;
        transform.DOJump(landPosition, jumpPower, 1, time).SetEase(Ease.InSine).OnComplete(() => StartCoroutine(Hit(landPosition)));
    }
    private IEnumerator Hit(Vector3 landPosition)
    {
        rotationTweener.Kill();
        Vector2 playerPos = MathHandler.Vector3ToVector2(GameManager.Instance.player.transform.position);
        Vector2 landPos = MathHandler.Vector3ToVector2(landPosition);

        if (Vector3.Distance(playerPos, landPos) <= hitRadius)
        {
            //Debug.Log("Hit");
        }
        else
        {
            //Debug.Log("Didnt Hit");
        }

        //Hit Effect
        hitExplosionParticles.Play();
        fireTrailParticles.Stop();
        float effectTime = hitExplosionParticles.main.duration;
        transform.DOScale(transform.localScale/2, effectTime);
        Material asteroidMaterial = thisRenderer.material;
        DOTween.To(() => asteroidMaterial.GetColor(VORONOI_COLOR), x => asteroidMaterial.SetColor(VORONOI_COLOR, x), Color.black, effectTime);
        DOTween.To(() => asteroidMaterial.GetColor(FRENSEL_COLOR), x => asteroidMaterial.SetColor(FRENSEL_COLOR , x), Color.black, effectTime);

        yield return new WaitForSeconds(effectTime);
        thisRenderer.enabled = false;
        hitExplosionParticles.Stop();

        gameObject.SetActive(false);
        shadowHitPoint.transform.SetParent(transform);
    }

    public void UpdateMe()
    {
        var delta = asteroidObject.position - prevPos;
        asteroidObject.rotation = Quaternion.LookRotation(delta, Vector3.up);
        prevPos = asteroidObject.position;
    }

    public void FixedUpdateMe()
    {
        
    }

    public void LateUpdateMe()
    {
    }
}
