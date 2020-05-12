using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySuckableObject : SuckableObject
{
    public override void Collected()
    {
        InventoryUI.instance.StartInventoryAddEffect(suckableobjectType, transform.position);
        base.Collected();
    }
}
