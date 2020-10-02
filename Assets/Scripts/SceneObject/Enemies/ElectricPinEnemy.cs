using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPinEnemy : AttackingEnemy
{
    [Header("ElectricPin")]
    [SerializeField] ParticleSystem prepareAttack;
    [SerializeField] float attackDelay = 4;
    [SerializeField] float radius = 10;
    [SerializeField] GameObject _electricityEffect;
    protected override void Awake()
    {
        base.Awake();
        pulledEvent += StopAttack;
        pulledEvent += () => _electricityEffect.SetActive(false);
        startDyingEvent += StopAttack;
    }
 
    protected override void OnEnable()
    {
        base.OnEnable();
        _electricityEffect.SetActive(true);
        StartCoroutine(SpawnInDelay());
        StartCoroutine(AttackCountdown());
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);

    }
    protected override IEnumerator Attack()
    {
        thisFollowPlayer.enabled = false;
        //ResetTransform();
        prepareAttack.Play();
        yield return new WaitForSeconds(attackDelay);
        if (health > 0)
        {
            Vector2 playerPos = MathHandler.Vector3ToVector2(GameManager.Instance.player.transform.position);
            Vector2 thisPos = MathHandler.Vector3ToVector2(transform.position);
            if (Vector2.Distance(thisPos, playerPos) < radius)
            {
                //Debug.Log("Hit");
            }
            else
            {
                //Debug.Log("Didnt Hit");

            }
            yield return new WaitForSeconds(0.5f);

            thisFollowPlayer.enabled = true;
            StartCoroutine(AttackCountdown());

        }
    }
    public override void StopAttack()
    {
        prepareAttack.Stop();
        prepareAttack.Clear();
        base.StopAttack();
    }
}
