using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
public class PlayerCar : PlayerBase
{
    [Header("Car")]
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] float divideForceBy=2;
    public override ePlayerType PlayerType { get => ePlayerType.Car; }

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
    public override void AddForce(Vector3 force)
    {
        base.AddForce(force/divideForceBy);
    }
}
