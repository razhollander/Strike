using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePinEnemy : Enemy
{
    public override SuckableObject Duplicate()
    {
        return this.Get<FirePinEnemy>();
    }
}
