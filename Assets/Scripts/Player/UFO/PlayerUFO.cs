using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUFO : PlayerBase
{
    private UFOVehicle _UFOVehicle;

    protected override void Awake()
    {
        base.Awake();
        _UFOVehicle = (UFOVehicle)_baseVehicle;
    }
    public override void AddForce(Vector2 force)
    {
        _UFOVehicle.MoveByDirection(force);
    }
}
