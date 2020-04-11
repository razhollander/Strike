﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BowlingBallShot : ObjectShot
{
    Tween rotationTweener;
    [SerializeField] private int EnemyHit = 3;
    private List<GameObject> enemiesHit;
    void Awake()
    {
        OnCollision += BowlingBallCollisionFunc;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        enemiesHit = new List<GameObject>();
        //DoSelfRotate();
    }
    private void BowlingBallCollisionFunc(Enemy enemy)
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
    private bool AlreadyHitEnemy(GameObject enemy)
    {
        return enemiesHit.Find(x => x == enemy) != null;
    }
}
