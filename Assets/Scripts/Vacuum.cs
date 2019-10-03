using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Vacuum : MonoBehaviour
{
    [SerializeField] GameObject AirParticals;
    [SerializeField] Transform VacuumPoint;
    [SerializeField] Transform VacuumHead;

    [SerializeField] float suckingPower = 2;
    [SerializeField] float vacuumRadius = 10;
    [SerializeField] float pullingSpeed = 1;
    [SerializeField] ParticleSystem sparksParticles;

    Vector2 vacuumPos;
    private Enemy EnemyBeingSucked;

    private Tween rotationTweener;
    private Tween shakeTweener;
    private Tween headShake;
    private Coroutine suckCoroutine;
    private Coroutine pullCoroutine;

    bool lookForClosestEnemy = false;
    bool isInSwallowAnimation = false;
    float swallowAnimationDuration = 0.25f;

    void Start()
    {
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
            vacuumPos = new Vector2(VacuumPoint.position.x, VacuumPoint.position.z);
            if (EnemyBeingSucked == null)
            {
                if (lookForClosestEnemy)
                    CheckForClosestEnemy();
                else
                    SuckEnemyIfInRadius();
            }
            else
            {
                if (!IsEnemyInRadius(EnemyBeingSucked, vacuumPos))
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
            if (IsEnemyInRadius(currentEnemy, vacuumPos))
            {
                StartSuckingEnemy(currentEnemy);
                return;
            }
        }
    }
    private bool CheckForClosestEnemy()
    {
        lookForClosestEnemy = false;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Enemy currentEnemy in allEnemies)
        {
            Vector2 enemyPos = new Vector2(currentEnemy.transform.position.x, currentEnemy.transform.position.z);
            float distanceToEnemy = (enemyPos - vacuumPos).sqrMagnitude;
            if (distanceToEnemy < Mathf.Pow(vacuumRadius, 2) && distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        if (closestEnemy != null)
        {
            StartSuckingEnemy(closestEnemy);
            return true;
        }
        return false;
    }
    private void StartSuckingEnemy(Enemy enemy)
    {
        EnemyBeingSucked = enemy;
        ShakeEnemy(enemy);
        headShake =VacuumHead.DOShakeRotation(1, 4, 15, 90, false).SetEase(Ease.Linear).SetLoops(-1);
        suckCoroutine = StartCoroutine(SuckEnemy());
        rotationTweener.Kill();
        AirParticals.SetActive(true);
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
        float distance = Vector3.Distance(EnemyBeingSucked.transform.position, VacuumPoint.position);
        float minDisance = 0.75f;

        while (distance > minDisance)
        {
            EnemyBeingSucked.transform.Rotate(rotationVec * Time.deltaTime, Space.Self);
            EnemyBeingSucked.transform.position += (VacuumPoint.position - EnemyBeingSucked.transform.position).normalized * pullingSpeed * Time.deltaTime;
            prevDistance = distance;
            distance = Vector3.Distance(EnemyBeingSucked.transform.position, VacuumPoint.position);
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
        AirParticals.SetActive(false);
        isInSwallowAnimation = true;
        sparksParticles.Play();
        swallowAnimationDuration = sparksParticles.main.startLifetime.constantMax;
        VacuumHead.DOPunchRotation((VacuumHead.right + VacuumHead.forward) * 10, swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    private void EndSwallowAnimation()
    {
        isInSwallowAnimation = false;
        sparksParticles.Stop();
        LookForNewEnemy();
    }

    private void LookForNewEnemy()
    {
        AirParticals.SetActive(false);
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
