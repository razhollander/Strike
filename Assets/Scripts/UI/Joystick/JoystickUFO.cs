using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUFO : Joystick
{
    [SerializeField] private UFOVehicle _UFOVehicle;

    //Vector3 _prevPos;
    //protected override void Awake()
    //{
    //    base.Awake();
    //    _UFOTransform = _UFOVehicle.transform;
    //    _movementSpeed = _UFOVehicle.GetMovementSpeed();
    //    _rotationSpeed = _UFOVehicle.GetRotationSpeed();
    //    _lerpPosAmount = _UFOVehicle.GetLerpPositionMultiplier();
    //    _lerpRotAmount = _UFOVehicle.GetLerpRotationMultiplier();
    //}

    public override void UpdateMe()
    {
        if (IsMouseHeld)
        {
            _UFOVehicle.MoveByDirection(Direction);
        }
   }
}
