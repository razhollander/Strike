using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePinEnemy : Enemy
{
    [Header("FirePin")]
    [SerializeField] private ParticleSystem trailEffect;
    public override SuckableObject Duplicate()
    {
        return this.Get<FirePinEnemy>();
    }
    protected override void Awake()
    {
        base.Awake();
        startDyingEvent += EndTrailEffect;
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
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
        trailEffect.Play();

    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
}
