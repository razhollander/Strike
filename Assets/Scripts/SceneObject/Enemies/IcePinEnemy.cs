using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePinEnemy : Enemy
{
    [Header("IcePin")]
    [SerializeField] private ParticleSystem trailEffect;

    public override SuckableObject Duplicate()
    {
        return this.Get<IcePinEnemy>();
    }
    public override void Start()
    {
        base.Start();
        OnStartDying += EndTrailEffect;
    }
    private void EndTrailEffect()
    {
        trailEffect.Stop();
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

    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
}
