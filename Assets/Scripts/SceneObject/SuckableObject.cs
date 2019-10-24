using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObject: PooledMonobehaviour
{
    [SerializeField]
    protected SuckableobjectType suckableobjectType;
    public static int age = 0;
    [System.NonSerialized] public bool isBeingSucked = false;
    public void Collected()
    {

        InventoryUI.instance.StartAddEffect(suckableobjectType,transform.position);
        Destroy(gameObject);
        suckableobjectType = SuckableobjectType.firePin;
        
    }
    private void Start()
    {
    }
}
public enum SuckableobjectType
{
    bowlingBall=0,
    normalPin=1,
    firePin=2,
    electricPin = 3,
    IcePin=4,
    powerUp =5
}
