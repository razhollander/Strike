using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Asteroid : PooledMonobehaviour
{
    [SerializeField] float time = 4;
    [SerializeField] float jumpPower = 5;
    [SerializeField] float hitRadius = 3;
    [SerializeField] float rotationSpeed=1;
    [SerializeField] float scaleSpeed;
    [SerializeField] Transform astroidObject;
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
    private void Awake()
    {
        Scale = transform.localScale;
        asteroidMaterial = thisRenderer.material;
        asteroidVoronoiColor = asteroidMaterial.GetColor("_voronoiColor");
        asteroidFrenselColor = asteroidMaterial.GetColor("_frenselColor");

    }
    private void OnEnable()
    {
        thisRenderer.enabled = true;
        asteroidMaterial.SetColor("_voronoiColor", asteroidVoronoiColor);
        asteroidMaterial.SetColor("_frenselColor", asteroidFrenselColor);
    }
    public void Lounch(Vector3 landPosition, Vector3 startPos)
    {
        //int times = 14 * (int)rotationSpeed;

        //int x = Random.Range(0, 360);
        //int y = Random.Range(0, 360);
        //int z = Random.Range(0, 360);
        Sequence animationSeq = DOTween.Sequence();
        animationSeq.PrependInterval(time / 2);
        animationSeq.Append(shadowHitPoint.transform.DOShakePosition(time/2, shadowShakeStrength, shadowShakeVabration, 90, false, false).SetEase(Ease.InExpo));
        //ShadowPoint
        shadowHitPoint.transform.SetParent(null);
        shadowHitPoint.transform.position = landPosition+Vector3.up*0.05f;
        shadowHitPoint.Play();
        //rotation
        transform.LookAt(new Vector3(landPosition.x,transform.position.y,landPosition.z));
        Vector3 rotationVec = new Vector3(100*rotationSpeed,0, 0);
        transform.localScale = startScale;
        transform.DOScale(Scale, scaleSpeed);
        rotationTweener = astroidObject.DORotate(rotationVec, time*1.2f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad);
        //Lounch
        transform.position = startPos;
        transform.DOJump(landPosition, jumpPower, 1, time).SetEase(Ease.InSine).OnComplete(() => StartCoroutine(Hit(landPosition)));
    }
    private IEnumerator Hit(Vector3 landPosition)
    {
        rotationTweener.Kill();
        Vector2 playerPos = MathHandler.Vector3ToVector2(GameManager.instance.player.transform.position);
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
        DOTween.To(() => asteroidMaterial.GetColor("_voronoiColor"), x => asteroidMaterial.SetColor("_voronoiColor",x), Color.black, effectTime);
        DOTween.To(() => asteroidMaterial.GetColor("_frenselColor"), x => asteroidMaterial.SetColor("_frenselColor", x), Color.black, effectTime);

        //asteroidMaterial.SetColor("_voronoiColor", Color.black);
        //asteroidMaterial.SetColor("_frenselColor", Color.black);

        yield return new WaitForSeconds(effectTime);
        thisRenderer.enabled = false;
        hitExplosionParticles.Stop();

        gameObject.SetActive(false);
        shadowHitPoint.transform.SetParent(transform);

    }
}
