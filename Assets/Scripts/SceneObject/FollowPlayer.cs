using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : OverridableMonoBehaviour
{
    
    private const float CHECK_FOR_COLLISION_BUFFER =15;
    private const float ANGLE_DELTA_DIR = 3;

    private GameObject player;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed = 1;

    Vector3 playerPos;
    LayerMask layermaskIgnored;
    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponentInChildren<Rigidbody>();
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        layermaskIgnored = ~((1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Player")));
    }

    public override void UpdateMe()
    {
        playerPos.Set(player.transform.position.x, transform.position.y, player.transform.position.z);
        Vector3 moveDelta = (playerPos - transform.position).normalized * speed * Time.deltaTime;
        float rayRadius = transform.localScale.x;
        Vector3 rayDir = moveDelta * CHECK_FOR_COLLISION_BUFFER * speed* rayRadius;
        Vector3 rayOrigin =new Vector3(transform.position.x, 0.5f, transform.position.z);
        RaycastHit rayhit;
        //Debug.DrawRay(rayOrigin, rayDir);
        if (Physics.SphereCast(rayOrigin, rayRadius, rayDir, out rayhit, rayDir.magnitude, layermaskIgnored))
        {
            moveDelta = GetDirNotColliding(rayOrigin, rayDir, rayRadius).normalized * speed * Time.deltaTime;
        }

        transform.position += moveDelta;
    }
    private Vector3 GetDirNotColliding(Vector3 origin,Vector3 dir,float scale)
    {
        Vector3 dirRight= dir;
        Vector3 dirLeft= dir;

        float magntiude = dir.magnitude;

        RaycastHit rayhit;

        for (int i = 0; i < 360/ANGLE_DELTA_DIR; i++)
        {
            dirRight = MathHandler.RotateVectorByAngle(dirRight, -ANGLE_DELTA_DIR);
            dirLeft = MathHandler.RotateVectorByAngle(dirLeft, ANGLE_DELTA_DIR);

            //Debug.DrawRay(origin, dirRight,Color.red);
            if (!Physics.SphereCast(origin, scale, dirRight, out rayhit, magntiude, layermaskIgnored))
            {
                return dirRight;
            }
            Debug.DrawRay(origin, dirLeft, Color.red);
            if (!Physics.SphereCast(origin, scale, dirLeft, out rayhit, magntiude, layermaskIgnored))
            {
                return dirLeft;
            }
        }
        return Vector3.zero;
    }
}
