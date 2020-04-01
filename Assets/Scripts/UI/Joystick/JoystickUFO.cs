using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUFO : Joystick
{
    [SerializeField] private UFOVehicle _UFOVehicle;

    private bool _isInNormalPlayState = false;
    private Transform _UFOTransform;
    Vector3 _newPos;
    Quaternion _newRot;
    float _movementSpeed;
    float _lerpPosAmount;
    float _rotationSpeed;
    float _lerpRotAmount;
    protected override void Awake()
    {
        base.Awake();
        _UFOTransform = _UFOVehicle.transform;
        _movementSpeed = _UFOVehicle.GetMovementSpeed();
        _rotationSpeed = _UFOVehicle.GetRotationSpeed();
        _lerpPosAmount = _UFOVehicle.GetLerpPositionMultiplier();
        _lerpRotAmount = _UFOVehicle.GetLerpRotationMultiplier();
    }
    protected override void Start()
    {
        base.Start();
        _newPos = _UFOTransform.position;
        _newRot = _UFOTransform.localRotation;
    }

    public override void UpdateMe()
    {
        if (IsMouseHeld)
        {
            _newPos += Direction.ToVector3() * _movementSpeed;
        }
        _UFOTransform.position = Vector3.Lerp(_UFOTransform.position, _newPos, _lerpPosAmount * Time.deltaTime);
        Vector3 deltaVector = (_newPos - _UFOTransform.position);
        Vector3 newDirection = Vector3.RotateTowards(_UFOTransform.forward, deltaVector, _rotationSpeed * Time.deltaTime, 0.0f);
        _UFOTransform.rotation = Quaternion.Lerp(_UFOTransform.rotation, Quaternion.LookRotation(newDirection), _lerpRotAmount);
    }
}
