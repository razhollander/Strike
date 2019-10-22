using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElectricPinShot : BasicPinShot
{
    [Header("ElectricPinShot")]
    [SerializeField] private float electricRadius;
    [SerializeField] ElectricStrike electricStrike;
    [SerializeField] GameObject trail;
    [SerializeField] float EffectDuration;
    Queue<Enemy> enemyHitQueue;
    List<Enemy> enemiesAlreadyHitList;
    List<ParticleSystem> electricStrikeParticlesList;

    //protected override void OnEnable()
    //{
    //    base.OnEnable();

    //}
    protected override void SetComponents(bool isEnabled)
    {
        base.SetComponents(isEnabled);
        trail.SetActive(isEnabled);
    }
    protected override IEnumerator PinCollisionFunc(Enemy enemy)
    {
        enemy.SetHealth(-damage, true, true);
        rotationTweener.Kill();
        ElectricStike(enemy);
        yield return null;
    }
    private void ElectricStike(Enemy enemy)
    {
        electricStrikeParticlesList = new List<ParticleSystem>();
        enemyHitQueue = new Queue<Enemy>();
        enemiesAlreadyHitList = new List<Enemy>();
        enemyHitQueue.Enqueue(enemy);
        enemiesAlreadyHitList.Add(enemy);
        while (enemyHitQueue.Count>0)
        {
            Enemy currEnemy = enemyHitQueue.Dequeue();
            Vector3 currEnemyPos = currEnemy.transform.position;
            Collider[] colliders = Physics.OverlapSphere(currEnemyPos, electricRadius);
            foreach (Collider collider in colliders)
            {
                
                Enemy enemyInRadius = collider.transform.GetComponent<Enemy>();
                if (enemyInRadius != null)
                {
                    if (enemyInRadius != currEnemy)
                    {
                        if (enemiesAlreadyHitList.Find(x => x == enemyInRadius) == null)
                        {
                            ElectricStrike currElectricStrike = electricStrike.Get<ElectricStrike>(true);
                            ParticleSystem lightningParticle = currElectricStrike.lightningParticle;
                            lightningParticle.Play();
                            electricStrikeParticlesList.Add(lightningParticle);
                            Vector3 enemyInRadiusPos = enemyInRadius.transform.position;
                            currElectricStrike.transform.position = currEnemyPos;
                            currElectricStrike.transform.LookAt(enemyInRadiusPos);
                            var main = currElectricStrike.lightningParticle.main;
                            main.startSpeed = Vector3.Distance(currEnemyPos, enemyInRadiusPos);
                            enemyInRadius.SetHealth(-damage, true, true);
                            enemyHitQueue.Enqueue(enemyInRadius);
                            enemiesAlreadyHitList.Add(enemyInRadius);
                        }
                    }
                }

            }
        }
        StartCoroutine(DisablePatricles());
        StartCoroutine(DestroySelf(EffectDuration+0.1f));
    }
    private IEnumerator DisablePatricles()
    {
        yield return new WaitForSeconds(EffectDuration);
        foreach (ParticleSystem particle in electricStrikeParticlesList)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }
}