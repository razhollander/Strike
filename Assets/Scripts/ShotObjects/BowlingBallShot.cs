using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BowlingBallShot : ObjectShot
{
    [SerializeField] protected int EnemyHit = 3;
    protected List<GameObject> enemiesHit;

    protected override void OnEnable()
    {
        base.OnEnable();
        enemiesHit = new List<GameObject>();
    }
    protected override void HandleCollision(Enemy enemy, Vector3 collisionPoint)
    {
        if (!AlreadyHitEnemy(enemy.gameObject))
        {
            EnemyHit--;
            enemiesHit.Add(enemy.gameObject);
            //Debug.Log(EnemyHit);
            enemy.SetHealth(-damage, true, true);
            if (EnemyHit == 0)
                StartCoroutine(DestroySelf());
        }
    }
    protected bool AlreadyHitEnemy(GameObject enemy)
    {
        return enemiesHit.Find(x => x == enemy) != null;
    }
}
