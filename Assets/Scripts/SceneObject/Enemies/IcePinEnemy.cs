using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePinEnemy : Enemy
{
    [Header("IcePin")]
    [SerializeField] private ParticleSystem trailEffect;
    [SerializeField] private ParticleSystem attackCircleEffect;

    //public override SuckableObject Duplicate()
    //{
    //    return this.Get<IcePinEnemy>();
    //}
    protected override void Awake()
    {
        base.Awake();
        startDyingEvent += EndEffects;
        pulledEvent += EndEffects;
    }
    private void EndEffects()
    {
        trailEffect.Stop();
        attackCircleEffect.Stop();
        attackCircleEffect.Clear();
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
        trailEffect.Play();
        attackCircleEffect.Play();

    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
}
