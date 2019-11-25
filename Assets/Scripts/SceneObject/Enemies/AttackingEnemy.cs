using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackingEnemy : Enemy
{
    [Header("Attacking Enemy")]

    [SerializeField] [Range(1, 20)] protected float minAttackTimer;
    [SerializeField] [Range(1, 20)] protected float maxAttackTimer;

    protected Coroutine attackCoroutine;

    public virtual void StopAttack()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }
    protected IEnumerator AttackCountdown()
    {
        float time = Random.Range(minAttackTimer, maxAttackTimer);
        yield return new WaitForSeconds(time);
        if (!IsBeingSucked && health > 0)
            attackCoroutine = StartCoroutine(Attack());
        else
            StartCoroutine(AttackCountdown());
    }
    protected abstract IEnumerator Attack();
}
