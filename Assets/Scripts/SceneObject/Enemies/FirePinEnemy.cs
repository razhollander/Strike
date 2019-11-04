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
        trailEffect.Play();
    }
}
