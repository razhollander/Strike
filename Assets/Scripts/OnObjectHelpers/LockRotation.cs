using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : OverridableMonoBehaviour
{
    Vector3 startRot;
    // Update is called once per frame
    protected override void Awake()
    {
        startRot = transform.rotation.eulerAngles;
        base.Awake();
    }
    public override void UpdateMe()
    {
        transform.rotation = Quaternion.Euler(startRot);
    }
}
