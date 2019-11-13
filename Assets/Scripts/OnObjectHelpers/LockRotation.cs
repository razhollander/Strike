using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : OverridableMonoBehaviour
{
    // Update is called once per frame
    public override void UpdateMe()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
