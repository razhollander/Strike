using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScale : OverridableMonoBehaviour
{
    const float ONE = 1;
    [SerializeField] bool _flipXY;
    [SerializeField] Transform _transformRelative;

    // Update is called once per frame
    public override void UpdateMe()
    {
        var relativeScale = _transformRelative.localScale;
        if(_flipXY)
            transform.localScale = new Vector3(ONE / relativeScale.y, ONE / relativeScale.x, ONE / relativeScale.z);
        else
            transform.localScale = new Vector3(ONE / relativeScale.x, ONE / relativeScale.y, ONE / relativeScale.z);
    }
}
