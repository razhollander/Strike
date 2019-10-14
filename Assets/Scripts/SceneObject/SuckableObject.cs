using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObject: PooledMonobehaviour
{
    [SerializeField]
    private SuckableobjectType suckableobjectType;
    [System.NonSerialized] public bool isBeingSucked = false;
    public void Collected()
    {
        InventoryUI.instance.StartAddEffect(suckableobjectType,transform.position);
        Destroy(gameObject);
    }
}
public enum SuckableobjectType
{
    bowlingBall,
    normalPin,
    firePin,
    powerUp
}
