using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : OverridableMonoBehaviour
{
    private GameObject player;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed = 1;
    Vector3 lookAtVector;
    Vector3 playerPos;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponentInChildren<Rigidbody>();
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

    }
    private void OnEnable()
    {
        //lookAtVector = new 
        //transform.LookAt(playerPos, Vector3.up);
    }
    //void FixedUpdate()
    //{
    //    rigidBody.velocity = (player.transform.position - transform.position) *speed;
    //}
    public override void UpdateMe()
    {
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
        playerPos.Set(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
