using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBowlingBall : HealthySuckableObject
{
    [SerializeField] GameObject _rainbowRingFX;

    protected override void Awake()
    {
        base.Awake();
        pulledEvent += ()=>_rainbowRingFX.SetActive(false);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _rainbowRingFX.SetActive(true);
    }
}
