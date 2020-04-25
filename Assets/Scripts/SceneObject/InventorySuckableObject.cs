using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySuckableObject : SuckableObject
{
    public override void Collected()
    {
        InventoryUI.instance.StartAddEffect(suckableobjectType, transform.position);
        base.Collected();
    }
}
