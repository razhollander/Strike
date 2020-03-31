using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOVehicle : BaseVehicle
{
    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _rotationSpeed = 1;
    [SerializeField] float _lerpPositionMultiplier = 1f;
    [SerializeField] float _lerpRotationMultiplier = 1f;

    public float GetMovementSpeed()
    {
        return _movementSpeed;
    }
    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }
    public float GetLerpPositionMultiplier()
    {
        return _lerpPositionMultiplier;
    }
    public float GetLerpRotationMultiplier()
    {
        return _lerpRotationMultiplier;
    }
}
