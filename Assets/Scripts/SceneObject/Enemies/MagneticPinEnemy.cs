using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPinEnemy : AttackingEnemy
{
    [Header("MagnecticPin")]
    [SerializeField] private ParticleSystem _attackEffect;
    [SerializeField] Transform _magnet;
    [SerializeField] private float _pullAmount;
    [SerializeField] private float _attackDauration = 3;

    PlayerBase _player;
    Coroutine pullPlayerCoroutine;
    Vector3 towardsPlayer;
    public override SuckableObject Duplicate()
    {
        return this.Get<MagneticPinEnemy>();
    }
    protected override void Awake()
    {
        base.Awake();
        pulledEvent += StopAttack;
        startDyingEvent += StopAttack;
    }
    private void Start()
    {
        _player = GameManager.Instance.player;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SpawnInDelay());
        StartCoroutine(AttackCountdown());
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
    protected override IEnumerator Attack()
    {
        _attackEffect.Play();
        pullPlayerCoroutine = StartCoroutine(PullPlayer());
        yield return new WaitForSeconds(_attackDauration);
        //yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime>lounchAtAnimationPercent);

        //if (health > 0)
        //{
        //    Asteroid newAstroid = astroid.Get<Asteroid>();
        //    Vector3 hitPos = GameManager.Instance.player.transform.forward * 15 + GameManager.Instance.player.transform.position;
        //    newAstroid.Lounch(hitPos, lounchAndroidPos.position);
        //    StartCoroutine(AttackCountdown());
        //}
        StopAttack();
        //yield return new WaitForSeconds(0.5f);

        //if (health>0)
        //    thisFollowPlayer.enabled = true;
    }
    private IEnumerator PullPlayer()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            towardsPlayer = (_player.transform.position - transform.position).SetYZero();
            _player.AddForce(-towardsPlayer.ToVector2() * _pullAmount);
            _magnet.LookAt(_player.transform);
        }
    }
    public override void StopAttack()
    {
        if (pullPlayerCoroutine != null)
        {
            StopCoroutine(pullPlayerCoroutine);
        }
        _attackEffect.Stop();
        base.StopAttack();
    }
}
