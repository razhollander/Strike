using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Vacuum : MonoBehaviour
{
    [SerializeField] GameObject airParticals;
    [SerializeField] Transform vacuumPoint;
    [SerializeField] Transform vacuumHead;
    [SerializeField] Transform radiusCenter;
    [SerializeField] float suckingPower = 2;
    [SerializeField] float vacuumRadius = 10;
    [SerializeField] float pullingSpeed = 1;
    [SerializeField] ParticleSystem sparksParticles;

    //Vector2 vacuumPointV2;
    Vector2 radiusCenterV2;
    private Enemy EnemyBeingSucked;

    private Tween rotationTweener;
    private Tween shakeTweener;
    private Tween headShake;
    private Coroutine suckCoroutine;
    private Coroutine pullCoroutine;

    bool lookForClosestEnemy = false;
    bool isInSwallowAnimation = false;
    float swallowAnimationDuration = 0.25f;

    private void Awake()
    {
       // vacuumPointV2= new Vector2(vacuumPoint.position.x, vacuumPoint.position.z);
        radiusCenterV2 = new Vector2(radiusCenter.position.x, radiusCenter.position.z);
        StartSelfRotation();
    }
    private void StartSelfRotation()
    {
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    void Update()
    {
        if (!isInSwallowAnimation)
        {
            //vacuumPointV2.Set(vacuumPoint.position.x, vacuumPoint.position.z);
            radiusCenterV2.Set(radiusCenter.position.x, radiusCenter.position.z);

            if (EnemyBeingSucked == null)
            {
                if (lookForClosestEnemy)
                    CheckForClosestEnemy();
                else
                    SuckEnemyIfInRadius();
            }
            else
            {
                if (!IsEnemyInRadius(EnemyBeingSucked, radiusCenterV2))
                {
                    StopSuckingEnemy();
                }
            }
        }
    }
    private void SuckEnemyIfInRadius()
    {
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy currentEnemy in allEnemies)
        {
            if (IsEnemyInRadius(currentEnemy, radiusCenterV2))
            {
                StartSuckingEnemy(currentEnemy);
                return;
            }
        }
    }
    private void CheckForClosestEnemy()
    {
        lookForClosestEnemy = false;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        Vector2 enemyPos = Vector2.zero;
        float distanceToEnemy = 0;
        foreach (Enemy currentEnemy in allEnemies)
        {
            enemyPos.Set(currentEnemy.transform.position.x, currentEnemy.transform.position.z);
            distanceToEnemy = (enemyPos - radiusCenterV2).sqrMagnitude;
            if (distanceToEnemy < Mathf.Pow(vacuumRadius, 2) && distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        if (closestEnemy != null)
        {
            StartSuckingEnemy(closestEnemy);
        }
        else
        {
            radiusCenter.gameObject.SetActive(true);
            airParticals.SetActive(false);
        }
    }
    private void StartSuckingEnemy(Enemy enemy)
    {
        EnemyBeingSucked = enemy;
        ShakeEnemy(enemy);
        headShake =vacuumHead.DOShakeRotation(1, 4, 15, 90, false).SetEase(Ease.Linear).SetLoops(-1);
        suckCoroutine = StartCoroutine(SuckEnemy());
        rotationTweener.Kill();
        airParticals.SetActive(true);
        radiusCenter.gameObject.SetActive(false);
    }
    private void ShakeEnemy(Enemy enemy)
    {
        shakeTweener = enemy.transform.DOShakeRotation(4, 10, 6, 70, false).SetEase(Ease.Linear).SetLoops(-1);
    }
    private IEnumerator SuckEnemy()
    {
        while (EnemyBeingSucked.health > 0)
        {
            transform.LookAt(new Vector3(EnemyBeingSucked.transform.position.x, transform.position.y, EnemyBeingSucked.transform.position.z), Vector3.up);
            EnemyBeingSucked.SetHealth(-suckingPower * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(PullEnemy());
    }
    IEnumerator PullEnemy()
    {
        shakeTweener.Kill();
        int times = 14 * (int)pullingSpeed;
        int x = Random.Range(1 * times, 2 * times);
        int y = Random.Range(1 * times, 2 * times);
        int z = Random.Range(1 * times, 2 * times);
        Vector3 rotationVec = new Vector3(x, y, z);

        float prevDistance = 0;
        float distance = Vector3.Distance(EnemyBeingSucked.transform.position, vacuumPoint.position);
        float minDisance = 0.75f;

        while (distance > minDisance)
        {
            EnemyBeingSucked.transform.Rotate(rotationVec * Time.deltaTime, Space.Self);
            EnemyBeingSucked.transform.position += (vacuumPoint.position - EnemyBeingSucked.transform.position).normalized * pullingSpeed * Time.deltaTime;
            prevDistance = distance;
            distance = Vector3.Distance(EnemyBeingSucked.transform.position, vacuumPoint.position);
            if (prevDistance < distance && distance > minDisance)
            {
                distance = 0;
            }
            else
            {
                EnemyBeingSucked.transform.localScale = Vector3.one * distance / vacuumRadius;
                transform.LookAt(new Vector3(EnemyBeingSucked.transform.position.x, transform.position.y, EnemyBeingSucked.transform.position.z), Vector3.up);
            }
            yield return null;
        }
        FinishPullingEnemy();
    }
    private void FinishPullingEnemy()
    {
        EnemyBeingSucked.Collected();
        headShake.Restart();
        headShake.Kill();
        DoSwallowFX();
    }
    private void DoSwallowFX()
    {
        airParticals.SetActive(false);
        isInSwallowAnimation = true;
        sparksParticles.Play();
        swallowAnimationDuration = sparksParticles.main.startLifetime.constantMax;
        vacuumHead.DOPunchRotation((vacuumHead.right + vacuumHead.forward) * 10, swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    private void EndSwallowAnimation()
    {
        isInSwallowAnimation = false;
        sparksParticles.Stop();
        LookForNewEnemy();
    }

    private void LookForNewEnemy()
    {

        shakeTweener.Restart();
        shakeTweener.Kill();
        headShake.Restart();
        headShake.Kill();
        EnemyBeingSucked = null;
        StartSelfRotation();
        lookForClosestEnemy = true;
    }
    private void StopSuckingEnemy()
    {
        EnemyBeingSucked.ResetHealth();
        StopCoroutine(suckCoroutine);
        LookForNewEnemy();
    }
    private bool IsEnemyInRadius(Enemy enemy, Vector2 myPos)
    {
        Vector2 enemyPos = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
        float distanceToEnemy = (enemyPos - myPos).sqrMagnitude;
        if (distanceToEnemy < Mathf.Pow(vacuumRadius, 2))
        {
            return true;
        }
        return false;
    }

}
