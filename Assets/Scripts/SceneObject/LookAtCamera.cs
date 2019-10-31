using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : OverridableMonoBehaviour
{

    public override void LateUpdateMe()
    {
        transform.LookAt(Camera.main.transform);
    }
}
