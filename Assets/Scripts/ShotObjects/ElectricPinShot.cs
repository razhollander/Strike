using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ElectricPinShot : BasicPinShot
{
    [Header("ElectricPinShot")]
    [SerializeField] private float electricRadius;
    //[SerializeField] private float strikeDelay;
    [SerializeField] ElectricStrike electricStrike;
    [SerializeField] GameObject trail;
    [Range(0,0.99f)]
    [SerializeField] float damageSubtracted;
    //[SerializeField] float EffectDuration;
    [SerializeField] float forceHitPower;
    Queue<EnemyHit> enemyHitQueue;
    List<Enemy> enemiesAlreadyHitList;
    //List<ParticleSystem> electricStrikeParticlesList;

        
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
        //yield return new WaitForSeconds(EffectDuration);
        StartCoroutine(DestroySelf());
        yield return null;
    }
    
    private void ElectricStike(Enemy enemy)
    {
        //electricStrikeParticlesList = new List<ParticleSystem>();
        enemyHitQueue = new Queue<EnemyHit>();
        enemiesAlreadyHitList = new List<Enemy>();
        enemyHitQueue.Enqueue(new EnemyHit(enemy,0));
        enemiesAlreadyHitList.Add(enemy);
        Vector3 force;
        while (enemyHitQueue.Count>0)
        {
            EnemyHit currEnemyHit = enemyHitQueue.Dequeue();
             
            Vector3 currEnemyPos = currEnemyHit.enemy.transform.position;
            Collider[] colliders = Physics.OverlapSphere(currEnemyPos, electricRadius);
            foreach (Collider collider in colliders)
            {
                
                Enemy enemyInRadius = collider.transform.GetComponent<Enemy>();
                if (enemyInRadius != null)
                {
                    if (enemyInRadius != currEnemyHit.enemy)
                    {
                        if (enemiesAlreadyHitList.Find(x => x == enemyInRadius) == null)
                        {
                            ElectricStrike currElectricStrike = electricStrike.Get<ElectricStrike>(true);
                            ParticleSystem lightningParticle = currElectricStrike.particle;
                            //electricStrikeParticlesList.Add(lightningParticle);
                            Vector3 enemyInRadiusPos = enemyInRadius.transform.position;
                            currElectricStrike.transform.position = currEnemyPos;
                            currElectricStrike.transform.LookAt(enemyInRadiusPos);
                            var main =lightningParticle.main;
                            main.startSpeed = Vector3.Distance(currEnemyPos, enemyInRadiusPos);
                            float delay = currEnemyHit.numInQueue;/// main.simulationSpeed;
                            main.startDelay = delay;
                            force = currElectricStrike.transform.forward * forceHitPower;
                            lightningParticle.Play();
                            float dealtDamage = Mathf.Clamp( -damage*Mathf.Pow(damageSubtracted,(currEnemyHit.numInQueue + 1)),-damage,0);
                            enemyInRadius.SetHealth(dealtDamage, true, true ,(currEnemyHit.numInQueue+1) / main.simulationSpeed);
                            enemyInRadius.AddForce(force, currEnemyHit.numInQueue / main.simulationSpeed);

                            enemyHitQueue.Enqueue(new EnemyHit(enemyInRadius, currEnemyHit.numInQueue+1));
                            enemiesAlreadyHitList.Add(enemyInRadius);
                        }
                    }
                }

            }
        }
    }
    private class EnemyHit
    {
        public Enemy enemy;
        public int numInQueue;

        public EnemyHit(Enemy enemy, int numInQueue)
        {
            this.enemy = enemy;
            this.numInQueue = numInQueue;
        }
    }
    //private void DisablePatricles()
    //{
    //    foreach (ParticleSystem particle in electricStrikeParticlesList)
    //    {
    //        particle.Stop();
    //        particle.gameObject.SetActive(false);
    //    }
    //}
}