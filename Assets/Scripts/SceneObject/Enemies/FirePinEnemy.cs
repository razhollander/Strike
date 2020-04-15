using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePinEnemy : AttackingEnemy
{
    [Header("FirePin")]
    [SerializeField] private ParticleSystem trailEffect;
    [SerializeField] private Animator animator;
    [SerializeField] Transform lounchAndroidPos;
    [SerializeField] Asteroid astroid;
    [SerializeField] [Range(0,1)] private float lounchAtAnimationPercent=0.8f;
    //public override SuckableObject Duplicate()
    //{
    //    return this.Get<FirePinEnemy>();
    //}
    protected override void Awake()
    {
        base.Awake();
        pulledEvent += StopAttack;
        pulledEvent += EndTrailEffect;
        startDyingEvent += EndTrailEffect;
        startDyingEvent += StopAttack;
    }
    private void EndTrailEffect()
    {
        trailEffect.Stop();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        trailEffect.Play();
        StartCoroutine(SpawnInDelay());
        StartCoroutine(AttackCountdown());

    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
        trailEffect.Play();
        //StartCoroutine(Attack());

    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
    protected override IEnumerator Attack()
    {
        thisFollowPlayer.enabled = false;
        animator.Play("ChargeAttack");

        yield return null;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime>lounchAtAnimationPercent);

        if (health > 0)
        {
            Asteroid newAstroid = astroid.Get<Asteroid>();
            Vector3 hitPos = GameManager.Instance.player.transform.forward * 15 + GameManager.Instance.player.transform.position;
            newAstroid.Lounch(hitPos, lounchAndroidPos.position);
            StartCoroutine(AttackCountdown());
        }

        yield return new WaitForSeconds(0.5f);

        if (health>0)
            thisFollowPlayer.enabled = true;
    }
    public override void StopAttack()
    {
        animator.Play("DefaultState");
        base.StopAttack();
    }
}
