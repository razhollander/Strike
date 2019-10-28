using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPinEnemy : Enemy
{
    public override SuckableObject Duplicate()
    {
        return this.Get<ElectricPinEnemy>();
    }
}
