using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : OverridableMonoBehaviour
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    // Update is called once per frame
    public override void UpdateMe()
    {
        Debug.Log("Hi");
    }
}
