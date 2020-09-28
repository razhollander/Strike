using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Threading.Tasks;
public class Enemy : HealthySuckableObject
{
    const string  IS_HIT = "_IsHit";

    [SerializeField] protected FollowPlayer thisFollowPlayer;

    [SerializeField] protected float spawnTimeDelay = 0.2f;
    private static float _hitEffectDuration = 0.3f;
    private float timeToDie = 3;
    protected event Action dieEvent;
    protected event Action startDyingEvent;
    private WaitForSeconds _hitWaitForSeconds;
    private Coroutine hitEffectCoroutine;

    protected override void Awake()
    {
        base.Awake();
        startDyingEvent += StartDying;
        OnBeingHit += DoHitEffect;

        _hitWaitForSeconds = new WaitForSeconds(_hitEffectDuration);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        thisFollowPlayer.enabled = true;

        if (_material.HasProperty(IS_HIT))
        {
            _material.SetInt(IS_HIT, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            startDyingEvent();
        }
    }

    private void DoHitEffect()
    {
        if (!_material.HasProperty(IS_HIT))
            return;

        if (hitEffectCoroutine != null)
            StopCoroutine(hitEffectCoroutine);

        hitEffectCoroutine = StartCoroutine(DoHitEffectCoroutine());
    }
    private IEnumerator DoHitEffectCoroutine()
    {
        _material.SetInt(IS_HIT, 1);
        yield return _hitWaitForSeconds;
        _material.SetInt(IS_HIT, 0);
    }
    private void StartDying()
    {
        StartCoroutine(StartDyingCoroutine());
    }
    private IEnumerator StartDyingCoroutine()
    {
        SetHealth(0, false, true);
        thisFollowPlayer.enabled = false;
        yield return new WaitForSeconds(timeToDie);
        if(!IsBeingSucked)
             gameObject.SetActive(false);
    }

    public void AddForce(Vector3 force,float delay)
    {
        StartCoroutine(AddForceDelay(force, delay));
    }

    private IEnumerator AddForceDelay(Vector3 force,float delay)
    {
        yield return new WaitForSeconds(delay);
        thisRigidBody.AddForce(force, ForceMode.Force);
    }
   
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
    }
    protected override void MakeActive(bool isActive)
    {
        thisFollowPlayer.enabled = isActive;
        base.MakeActive(isActive);
    }
}
