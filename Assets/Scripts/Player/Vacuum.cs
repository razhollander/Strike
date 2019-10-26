using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Vacuum : MonoBehaviour
{
    [SerializeField] GameObject airParticals;
    [SerializeField] protected Transform vacuumPoint;
    [SerializeField] protected Transform vacuumHead;
    [SerializeField] Transform radiusCenter;
    [SerializeField] float suckingPower = 2;
    [SerializeField] float vacuumRadius = 10;
    [SerializeField] float pullingSpeed = 1;
    [SerializeField] ParticleSystem sparksParticles;
    [SerializeField] Transform vacuumButton;
    //Vector2 vacuumPointV2;
    Vector2 radiusCenterV2;
    protected SuckableObject ObjectBeingSucked;

    protected Tween rotationTweener;
    private Tween shakeTweener;
    private Tween headShake;
    private Coroutine suckCoroutine;
    private Coroutine pullCoroutine;

    protected bool isInPulling = false;
    public bool vaccumButtonPressed { get; set; }
    float swallowAnimationDuration = 0.25f;
    private void Awake()
    {
        // vacuumPointV2= new Vector2(vacuumPoint.position.x, vacuumPoint.position.z);
        radiusCenterV2 = new Vector2(radiusCenter.position.x, radiusCenter.position.z);
        EventTrigger trigger = vacuumButton.GetComponent<EventTrigger>();

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
    private void OnButtonDown()
    {
        vaccumButtonPressed = true;
    }
    private void OnButtonUp()
    {
        vaccumButtonPressed = false;
    }
    protected void StartSelfRotation()
    {
        rotationTweener.Kill();
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    void Update()
    {
        if (!isInPulling && vaccumButtonPressed)
        {
            if (ObjectBeingSucked == null)
            {
                SuckableObject closestEnemy = CheckForClosestEnemy();
                if (closestEnemy != null)
                {
                    StartSuckingEnemy(closestEnemy);
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
        radiusCenterV2.Set(radiusCenter.position.x, radiusCenter.position.z);
        foreach (SuckableObject currentEnemy in allEnemies)
        {
            if (!currentEnemy.isBeingSucked)
                if (IsEnemyInRadius(currentEnemy, out distanceToEnemy))
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestEnemy = currentEnemy;
                    }
        }
        return closestEnemy;
    }
    private void StartSuckingEnemy(SuckableObject suckedObject)
    {
        ObjectBeingSucked = suckedObject;
        ObjectBeingSucked.isBeingSucked = true;
        ShakeEnemy(suckedObject);
        headShake = vacuumHead.DOShakeRotation(1, 4, 15, 90, false).SetEase(Ease.Linear).SetLoops(-1);
        suckCoroutine = StartCoroutine(SuckObject());
        rotationTweener.Kill();
        airParticals.SetActive(true);
    }
    private void ShakeEnemy(SuckableObject enemy)
    {
        shakeTweener = enemy.transform.DOShakeRotation(4, 10, 6, 70, false).SetEase(Ease.Linear).SetLoops(-1);
    }
    private IEnumerator SuckObject()
    {
        Vector3 lookatVec = new Vector3(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z);
        float tempDistance = 0;
        if (ObjectBeingSucked is Enemy)
        {
            Enemy enemyBeingSucked = (Enemy)ObjectBeingSucked;
            while (enemyBeingSucked.health > 0)
            {
                radiusCenterV2.Set(radiusCenter.position.x, radiusCenter.position.z);
                if (vaccumButtonPressed && IsEnemyInRadius(ObjectBeingSucked, out tempDistance))
                {
                    lookatVec.Set(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z);
                    transform.LookAt(lookatVec, Vector3.up);
                    if (enemyBeingSucked.CanBeSucked())
                        enemyBeingSucked.SetHealth(-suckingPower * Time.deltaTime);
                }
                else
                {
                    StopSuckingEnemy();
                }
                yield return null;
            }
        }
        StartCoroutine(PullEnemy());
    }
    IEnumerator PullEnemy()
    {
        isInPulling = true;
        shakeTweener.Kill();
        int times = 14 * (int)pullingSpeed;
        int x = Random.Range(1 * times, 2 * times);
        int y = Random.Range(1 * times, 2 * times);
        int z = Random.Range(1 * times, 2 * times);
        Vector3 rotationVec = new Vector3(x, y, z);

        float prevDistance = 0;
        float distance = Vector3.Distance(ObjectBeingSucked.transform.position, vacuumPoint.position);
        float minDisance = 0.75f;

        while (distance > minDisance)
        {
            ObjectBeingSucked.transform.Rotate(rotationVec * Time.deltaTime, Space.Self);
            ObjectBeingSucked.transform.position += (vacuumPoint.position - ObjectBeingSucked.transform.position).normalized * pullingSpeed * Time.deltaTime;
            prevDistance = distance;
            distance = Vector3.Distance(ObjectBeingSucked.transform.position, vacuumPoint.position);
            if (prevDistance < distance && distance > minDisance)
            {
                distance = 0;
            }
            else
            {
                ObjectBeingSucked.transform.localScale = Vector3.one * distance / vacuumRadius;
                transform.LookAt(new Vector3(ObjectBeingSucked.transform.position.x, transform.position.y, ObjectBeingSucked.transform.position.z), Vector3.up);
            }
            yield return null;
        }
        FinishPullingEnemy();
    }
    private void FinishPullingEnemy()
    {

        ObjectBeingSucked.Collected();
        headShake.Restart();
        headShake.Kill();
        DoSwallowFX();

    }
    private void DoSwallowFX()
    {
        rotationTweener.Kill();
        airParticals.SetActive(false);
        sparksParticles.Play();
        swallowAnimationDuration = sparksParticles.main.startLifetime.constantMax;
        vacuumHead.DOPunchRotation((vacuumHead.right + vacuumHead.forward) * 10, swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    private void EndSwallowAnimation()
    {
        isInPulling = false;
        sparksParticles.Stop();
        ResetParameters();
    }
    private void ResetParameters()
    {
        shakeTweener.Restart();
        shakeTweener.Kill();
        headShake.Restart();
        headShake.Kill();
        ObjectBeingSucked.isBeingSucked = false;
        ObjectBeingSucked = null;
        //radiusCenter.gameObject.SetActive(true);
        airParticals.SetActive(false);
        StartSelfRotation();
    }
    public void StopSuckingEnemy()
    {
        Enemy enemyBeingSucked = (Enemy)ObjectBeingSucked;
        enemyBeingSucked.ResetHealth();
        StopCoroutine(suckCoroutine);
        ResetParameters();
    }
    private bool IsEnemyInRadius(SuckableObject enemy, out float sqrMagnitudeDistance)
    {
        Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
        sqrMagnitudeDistance = (enemyPos - radiusCenterV2).sqrMagnitude;
        if (sqrMagnitudeDistance < Mathf.Pow(vacuumRadius, 2))
        {
            return true;
        }
        return false;
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
