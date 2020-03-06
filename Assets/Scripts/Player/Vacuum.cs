using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Vacuum : OverridableMonoBehaviour
{
    [Header("Vacuum")]

    [SerializeField] GameObject _airParticals;
    [SerializeField] protected Transform VacuumPoint;
    [SerializeField] protected Transform VacuumHead;
    [SerializeField] Transform _radiusCenter;
    [SerializeField] float _suckingPower = 2;
    [SerializeField] float _vacuumRadius = 10;
    [SerializeField] float _pullingSpeed = 1;
    [SerializeField] ParticleSystem _sparksParticles;
    [SerializeField] Transform _vacuumButton;
    [SerializeField] float _swallowCooldown;
    private float _swallowAnimationDuration = 0.25f;

    public bool VaccumButtonPressed { get;private set; }

    protected SuckableObject ObjectBeingSucked;
    protected Tween RotationTweener;
    protected bool IsInPulling { get; private set; }

    private Vector2 _radiusCenterV2;
    private Tween _shakeTweener;
    private Tween _headShake;
    private Coroutine _suckCoroutine;
    private Coroutine pullCoroutine;

    protected override void Awake()
    {
        base.Awake();
        IsInPulling = false;
        // vacuumPointV2= new Vector2(vacuumPoint.position.x, vacuumPoint.position.z);
        _radiusCenterV2 = new Vector2(_radiusCenter.position.x, _radiusCenter.position.z);
        EventTrigger trigger = _vacuumButton.GetComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener((data) => { OnButtonDown(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.PointerUp;
        entryUp.callback.AddListener((data) => { OnButtonUp(); });
        trigger.triggers.Add(entryUp);
        StartSelfRotation();
    }
    private void Start()
    {
        GameManager.Instance.OnGamePlayStart += SetVacuumParametersData;
    }
    private void SetVacuumParametersData()
    {
        var upgradesManagar = GameManager.Instance.UpgradesManager;
        _suckingPower = upgradesManagar.GetUpgrade<PowerUpgrader>().GetUpgradeValue();
        _pullingSpeed = upgradesManagar.GetUpgrade<SpeedUpgrader>().GetUpgradeValue();
        _vacuumRadius = upgradesManagar.GetUpgrade<RadiusUpgrader>().GetUpgradeValue();
    }
    private void OnButtonDown()
    {
        VaccumButtonPressed = true;
    }
    private void OnButtonUp()
    {
        VaccumButtonPressed = false;
    }
    protected void StartSelfRotation()
    {
        RotationTweener.Kill();
        RotationTweener = transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    public override void UpdateMe()
    {
        if (!IsInPulling && VaccumButtonPressed)
        {
            if (ObjectBeingSucked == null)
            {
                SuckableObject closestEnemy = CheckForClosestEnemy();
                if (closestEnemy != null)
                {
                    StartSuckingObject(closestEnemy);
                }
            }
        }
    }
    private SuckableObject CheckForClosestEnemy()
    {
        SuckableObject[] allEnemies = GameObject.FindObjectsOfType<SuckableObject>();
        SuckableObject closestEnemy = null;
        float closestDistance = float.MaxValue;
        float distanceToEnemy = 0;
        _radiusCenterV2.Set(_radiusCenter.position.x, _radiusCenter.position.z);
        foreach (SuckableObject currentEnemy in allEnemies)
        {
            if (!currentEnemy.IsBeingSucked)
                if (IsEnemyInRadius(currentEnemy, out distanceToEnemy))
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = currentEnemy;
                    }
        }
        return closestEnemy;
    }
    private void StartSuckingObject(SuckableObject suckedObject)
    {
        ObjectBeingSucked = suckedObject;
        ObjectBeingSucked.IsBeingSucked = true;
        ShakeObject(suckedObject);
        _headShake = VacuumHead.DOShakeRotation(1, 4, 15, 90, false).SetEase(Ease.Linear).SetLoops(-1);
        _suckCoroutine = StartCoroutine(SuckObject());
        RotationTweener.Kill();
        _airParticals.SetActive(true);
    }
    private void ShakeObject(SuckableObject suckedObject)
    {
        _shakeTweener = suckedObject.thisRenderer.transform.DOShakeRotation(4, 10, 6, 70, false).SetEase(Ease.Linear).SetLoops(-1);
    }
    private IEnumerator SuckObject()
    {
        Vector3 lookatVec = new Vector3(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z);
        float tempDistance = 0;
        if (ObjectBeingSucked is Enemy)
        {
            Enemy enemyBeingSucked = (Enemy)ObjectBeingSucked;
            while (enemyBeingSucked.Health > 0)
            {
                _radiusCenterV2.Set(_radiusCenter.position.x, _radiusCenter.position.z);
                if (VaccumButtonPressed && IsEnemyInRadius(ObjectBeingSucked, out tempDistance))
                {
                    lookatVec.Set(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z);
                    transform.LookAt(lookatVec, Vector3.up);
                    if (enemyBeingSucked.CanBeSucked())
                        enemyBeingSucked.SetHealth(-_suckingPower * Time.deltaTime);
                }
                else
                {
                    StopSuckingEnemy();
                }
                yield return null;
            }
        }
        StartCoroutine(PullObject());
    }
    IEnumerator PullObject()
    {
        IsInPulling = true;
        _shakeTweener.Kill();
        int times = 14 * (int)_pullingSpeed;
        int x = Random.Range(1 * times, 2 * times);
        int y = Random.Range(1 * times, 2 * times);
        int z = Random.Range(1 * times, 2 * times);
        Vector3 rotationVec = new Vector3(x, y, z);

        float prevDistance = 0;
        float distance = Vector3.Distance(ObjectBeingSucked.transform.position, VacuumPoint.position);
        float minDisance = 0.75f;
        ObjectBeingSucked.GetPulled();
        while (distance > minDisance)
        {
            ObjectBeingSucked.transform.Rotate(rotationVec * Time.deltaTime, Space.Self);
            ObjectBeingSucked.transform.position += (VacuumPoint.position - ObjectBeingSucked.transform.position).normalized * _pullingSpeed * Time.deltaTime;
            prevDistance = distance;
            distance = Vector3.Distance(ObjectBeingSucked.transform.position, VacuumPoint.position);
            if (prevDistance < distance && distance > minDisance)
            {
                distance = 0;
            }
            else
            {
                ObjectBeingSucked.transform.localScale = Vector3.one * distance / _vacuumRadius;
                transform.LookAt(new Vector3(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z), Vector3.up);
            }
            yield return null;
        }
        FinishPullingObject();
    }
    private void FinishPullingObject()
    {

        ObjectBeingSucked.Collected();
        _headShake.Restart();
        _headShake.Kill();
        DoSwallowFX();

    }
    private void DoSwallowFX()
    {
        RotationTweener.Kill();
        _airParticals.SetActive(false);
        _sparksParticles.Play();
        _swallowAnimationDuration = _sparksParticles.main.startLifetime.constantMax;
        VacuumHead.DOPunchRotation((VacuumHead.right + VacuumHead.forward) * 10, _swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    private void EndSwallowAnimation()
    {
        IsInPulling = false;
        _sparksParticles.Stop();
        ResetParameters();
    }
    private void ResetParameters()
    {
        _shakeTweener.Restart();
        _shakeTweener.Kill();
        _headShake.Restart();
        _headShake.Kill();
        ObjectBeingSucked.IsBeingSucked = false;
        ObjectBeingSucked = null;
        //radiusCenter.gameObject.SetActive(true);
        _airParticals.SetActive(false);
        StartSelfRotation();
    }
    public void StopSuckingEnemy()
    {
        Enemy enemyBeingSucked = (Enemy)ObjectBeingSucked;
        enemyBeingSucked.ResetHealth();
        StopCoroutine(_suckCoroutine);
        ResetParameters();
    }
    private bool IsEnemyInRadius(SuckableObject enemy, out float sqrMagnitudeDistance)
    {
        Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
        sqrMagnitudeDistance = (enemyPos - _radiusCenterV2).sqrMagnitude;
        if (sqrMagnitudeDistance < Mathf.Pow(_vacuumRadius, 2))
        {
            return true;
        }
        return false;
    }
    public void SetSuckingPower(float newPower)
    {
        _suckingPower = newPower;
    }
    public void SetPullingSpeed(float speed)
    {
        _pullingSpeed = speed;
    }

    //public void VaccumButtonPressed()
    //{
    //    radiusCenter.gameObject.SetActive(true);
    //    vaccumButtonPressed = true;
    //}
    //public void VaccumButtonReleased()
    //{
    //    radiusCenter.gameObject.SetActive(false);
    //    vaccumButtonPressed = false;

    //}
}
