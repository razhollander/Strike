using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed=1;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
    }

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    rigidBody.velocity = (player.transform.position - transform.position) *speed;
    //}
    void Update()
    {
        transform.position+= (player.transform.position - transform.position).normalized * speed*Time.deltaTime;
    }
}
