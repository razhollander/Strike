using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPinEnemy : Enemy
{
    [Header("ElectricPin")]
    [SerializeField] float spawnTimeDelay = 0.2f;
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
        childRenderer.enabled = false;
        yield return new WaitForSeconds(spawnTimeDelay);
        childRenderer.enabled = true;

    }
}
