using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickUFO : Joystick
{
    [SerializeField] private UFOVehicle _UFOVehicle;
    [SerializeField] private float rotateBy = 5;
    private bool _isInNormalPlayState = false;
    private Transform _UFOTransform;
    Vector3 _newPos;
    float _movementSpeed;
    float _lerpPosAmount;
    float _rotationSpeed;
    float _lerpRotAmount;
    //Vector3 _prevPos;
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
        Reset();
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += Reset;
    }

    private void Reset()
    {
        base.Start();
        _newPos = _UFOTransform.position;
        //_prevPos = _newPos;
    }

    public override void UpdateMe()
    {
        //_prevPos = _UFOTransform.position;

        if (IsMouseHeld)
        {
            _newPos += Direction.ToVector3() * _movementSpeed;
        }

        _UFOTransform.position = Vector3.Lerp(_UFOTransform.position, _newPos, _lerpPosAmount * Time.deltaTime);

        Vector3 deltaVector = (_newPos - _UFOTransform.position);
        Vector3 newDirection = Vector3.RotateTowards(new Vector3(_UFOTransform.forward.x, deltaVector.y, _UFOTransform.forward.z), deltaVector, _rotationSpeed * Time.deltaTime, 0.0f);
        Quaternion turnRotation =  Quaternion.LookRotation(newDirection);
        Quaternion leanForwardRotation = Quaternion.AngleAxis(rotateBy * deltaVector.magnitude,Vector3.right);
        _UFOTransform.rotation = Quaternion.Lerp(_UFOTransform.rotation, turnRotation * leanForwardRotation, _lerpRotAmount);
    }
}
