using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPinEnemy : Enemy
{
    [Header("ElectricPin")]
    [SerializeField] ParticleSystem prepareAttack;
    [SerializeField] ParticleSystem attackEffect;
    [SerializeField] float attackDelay = 4;
    [SerializeField] float radius = 10;
    [SerializeField] [Range(0, 20)] float minAttackCounter;
    [SerializeField] [Range(0, 20)] float maxAttackCounter;

    public override SuckableObject Duplicate()
    {
        return this.Get<ElectricPinEnemy>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SpawnInDelay());
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
        StartCoroutine(AttackCountdown());

    }
    private IEnumerator AttackCountdown()
    {
       float time= Random.Range(minAttackCounter, maxAttackCounter);
        yield return new WaitForSeconds(time);
        if (!IsBeingSucked)
            StartCoroutine(Attack());
        else
            StartCoroutine(AttackCountdown());

    }
    private IEnumerator Attack()
    {
        thisFollowPlayer.enabled = false;
        ResetTransform();
        prepareAttack.Play();
        yield return new WaitForSeconds(attackDelay);
        if (health > 0)
        {
            Vector2 playerPos = MathHandler.Vector3ToVector2(GameManager.instance.player.transform.position);
            Vector2 thisPos = MathHandler.Vector3ToVector2(transform.position);
            if (Vector2.Distance(thisPos, playerPos) < radius)
            {
                Debug.Log("Hit");
            }
            else
            {
                Debug.Log("Didnt Hit");

            }
            yield return new WaitForSeconds(0.5f);

            thisFollowPlayer.enabled = true;
            StartCoroutine(AttackCountdown());

        }

    }
}
