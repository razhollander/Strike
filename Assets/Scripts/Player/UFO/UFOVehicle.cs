using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOVehicle : BaseVehicle
{
    [SerializeField] private float _movementSpeed = 0.2f;
    [SerializeField] private float _rotationSpeed = 10;
    [SerializeField] float _lerpPositionMultiplier = 4;
    [SerializeField] float _lerpRotationMultiplier = 0.8f;
    [SerializeField] private float rotateBy = 4;

    Vector3 _newPos;

    public void MoveByDirection(Vector2 direction)
    {
        _newPos += direction.ToVector3() * _movementSpeed * Time.deltaTime;
    }
    private void Start()
    {
        Reset();
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += Reset;
    }
    private void Reset()
    {
        _newPos = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _newPos, _lerpPositionMultiplier * Time.deltaTime);

        Vector3 deltaVector = (_newPos - transform.position);
        Vector3 newDirection = Vector3.RotateTowards(new Vector3(transform.forward.x, deltaVector.y, transform.forward.z), deltaVector, _rotationSpeed * Time.deltaTime, 0.0f);
        Quaternion turnRotation = Quaternion.LookRotation(newDirection);
        Quaternion leanForwardRotation = Quaternion.AngleAxis(rotateBy * deltaVector.magnitude, Vector3.right);
        transform.rotation = Quaternion.Lerp(transform.rotation, turnRotation * leanForwardRotation, _lerpRotationMultiplier);
    }
    //public float GetMovementSpeed()
    //{
    //    return _movementSpeed;
    //}
    //public float GetRotationSpeed()
    //{
    //    return _rotationSpeed;
    //}
    //public float GetLerpPositionMultiplier()
    //{
    //    return _lerpPositionMultiplier;
    //}
    //public float GetLerpRotationMultiplier()
    //{
    //    return _lerpRotationMultiplier;
    //}
}
