using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SuperBowlingBallShot : BowlingBallShot
{
    [SerializeField] SuperBowlingBallStrike _hitEffect;
    
    protected override void HandleCollision(Enemy enemy, Vector3 collisionPoint)
    {
        if (!AlreadyHitEnemy(enemy.gameObject))
        {
            enemiesHit.Add(enemy.gameObject);
            enemy.SetHealth(-damage, true, true);
            Transform effect = _hitEffect.Get<SuperBowlingBallStrike>().transform;
            effect.position = collisionPoint;
        }
    }

}
