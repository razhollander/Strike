﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
public class PlayerCar : PlayerBase
{
    [SerializeField] Rigidbody _rigidbody;
    protected override void Awake()
    {
        _rigidbody.isKinematic = true;
        base.Awake();
    }
    protected override void SpawnAnimtion()
    {
        _rigidbody.isKinematic = true;
        _animator.enabled = true;
        base.SpawnAnimtion();
    }

    protected override void HandleEnterNormalPlay()
    {
        _rigidbody.isKinematic = false;
        _animator.enabled = false;
        base.HandleEnterNormalPlay();
    }

}
