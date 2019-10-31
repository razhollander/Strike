using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeHitBox : OverridableMonoBehaviour
{
    IcePinShot icePinShot;
    List<Enemy> enemies;
    static Vector3 beginScale;
    protected override void Awake()
    {
        base.Awake();
        beginScale = transform.localScale;
    }
    private void OnEnable()
    {
        enemies = new List<Enemy>();
    }
    public override void UpdateMe()
    {
        transform.localScale = beginScale * 1 / transform.parent.localScale.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy otherEnemy = other.transform.GetComponent<Enemy>();
        if (otherEnemy != null && enemies.Find(x=>x==otherEnemy)==null)
            enemies.Add(otherEnemy);
    }
    private void OnTriggerExit(Collider other)
    {
        Enemy otherEnemy = other.transform.GetComponent<Enemy>();
        if (otherEnemy != null)
            enemies.Remove(otherEnemy);
    }
    public List<Enemy> GetEnemiesInBounds()
    {
        return enemies;
    }
}
