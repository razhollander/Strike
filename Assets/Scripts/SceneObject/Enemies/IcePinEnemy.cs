using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePinEnemy : Enemy
{
    public override SuckableObject Duplicate()
    {
        return this.Get<IcePinEnemy>();
    }
}
