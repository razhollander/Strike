using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Vaccum : MonoBehaviour
{
    [SerializeField]
    private GameObject Particals;
    public float vacuumRadius = 10;
    private Enemy EnemyBeingSucked;
    private Tween rotationTweener;

    // Start is called before the first frame update
    void Start()
    {
        //transform.DOMove(new Vector3(50,50,50),5).From();
        //GetComponentInChildren<Material>().DOFade(1, 5);
        //rb.DOJump(transform.position + Vector3.left * 20, 10, 2, 2);
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyBeingSucked==null)
            CheckEnemyInRadius();
        else
        {
            Vector2 vaccumPos = new Vector2(transform.position.x, transform.position.z);
            if (IsEnemyInRadius(EnemyBeingSucked, vaccumPos))
            {
                Debug.DrawRay(transform.position, EnemyBeingSucked.transform.position- transform.position);
                transform.LookAt(new Vector3(EnemyBeingSucked.transform.position.x,transform.position.y, EnemyBeingSucked.transform.position.z),Vector3.up);
            }
            else
            {
                StopSuckingEnemy();
            }
        }
    }

    private void CheckEnemyInRadius()
    {
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();
        Vector2 vaccumPos = new Vector2(transform.position.x, transform.position.z);

        foreach(Enemy currentEnemy in allEnemies)
        {
            if(IsEnemyInRadius(currentEnemy,vaccumPos))
            {
                StartSuckingEnemy(currentEnemy);
                return;
            }
        }
    }
    private void StartSuckingEnemy(Enemy enemy)
    {
        EnemyBeingSucked = enemy;
        enemy.StartInVaccumRange();
        rotationTweener.Kill();
        Particals.SetActive(true);
    }
    private void StopSuckingEnemy()
    {
        EnemyBeingSucked.EndInVaccumRange();
        EnemyBeingSucked = null;
        Particals.SetActive(false);
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
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
