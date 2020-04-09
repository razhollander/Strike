using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUFO : PlayerBase
{
    [Header("UFO")]

    private UFOVehicle _UFOVehicle;

    public override ePlayerType PlayerType { get => ePlayerType.UFO; }

    protected override void Awake()
    {
        base.Awake();
        _UFOVehicle = (UFOVehicle)_baseVehicle;
    }
    public override void AddForce(Vector3 force)
    {
        _UFOVehicle.AddForce(force);
    }
}
