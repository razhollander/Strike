using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : OverridableMonoBehaviour
{
    
    private const float CHECK_FOR_COLLISION_BUFFER =20;
    private const float ANGLE_DELTA_DIR = 3;
    private const float RAY_HEIGHT_ABOVE_GROUND = 0.5f;
    private const int MAX_ANGLE = 360;
    private GameObject player;
    [SerializeField] private float _speed = 1;
    float _rayRadius;
    Vector3 playerPos;
    Vector3 moveDelta;
    Vector3 _rayDir;
    Vector3 _rayOrigin;
    RaycastHit _rayhit;
    LayerMask _layermaskIgnored;
    private float _timeDeltaTime;
    private Vector3 _dirRight;
    private Vector3 _dirLeft;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        _layermaskIgnored = ~((1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Player")));
        _rayRadius = transform.localScale.x;
        _rayOrigin = new Vector3(transform.position.x, RAY_HEIGHT_ABOVE_GROUND, transform.position.z);
    }

    public override void UpdateMe()
    {
        MoveTowardsPlayer();
    }
    private void MoveTowardsPlayer()
    {
        _timeDeltaTime = Time.deltaTime;
        playerPos.Set(player.transform.position.x, transform.position.y, player.transform.position.z);
        moveDelta = (playerPos - transform.position).normalized * _speed * _timeDeltaTime;
        _rayDir = moveDelta * CHECK_FOR_COLLISION_BUFFER * _speed * _rayRadius;
        _rayOrigin.Set(transform.position.x, RAY_HEIGHT_ABOVE_GROUND, transform.position.z);

        if (Physics.SphereCast(_rayOrigin, _rayRadius, _rayDir, out _rayhit, _rayDir.magnitude, _layermaskIgnored))
        {
            moveDelta = GetDirNotColliding(_rayOrigin, _rayDir).normalized * _speed * _timeDeltaTime;
        }

        transform.position += moveDelta;
    }
    private Vector3 GetDirNotColliding(Vector3 origin,Vector3 dir)
    {
        _dirRight= dir;
        _dirLeft= dir;
        float magntiude = dir.magnitude;

        for (int i = 0; i < MAX_ANGLE/ANGLE_DELTA_DIR; i++)
        {
            _dirRight = MathHandler.RotateVectorByAngle(_dirRight, -ANGLE_DELTA_DIR);
            _dirLeft = MathHandler.RotateVectorByAngle(_dirLeft, ANGLE_DELTA_DIR);

            if (!Physics.SphereCast(origin, _rayRadius, _dirRight, out _rayhit, magntiude, _layermaskIgnored))
            {
                return _dirRight;
            }
            if (!Physics.SphereCast(origin, _rayRadius, _dirLeft, out _rayhit, magntiude, _layermaskIgnored))
            {
                return _dirLeft;
            }
        }
        return Vector3.zero;
    }
}
