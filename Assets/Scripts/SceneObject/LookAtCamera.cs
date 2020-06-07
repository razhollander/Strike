using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : OverridableMonoBehaviour
{
    private Transform _cam;
    protected override void Awake()
    {
        base.Awake();
        _cam = Camera.main.transform;
    }
    public override void LateUpdateMe()
    {
        transform.LookAt(_cam);
    }
}
