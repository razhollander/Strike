using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float speed=1;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Awake()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
        playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

    }
    private void OnEnable()
    {
        transform.LookAt(playerPos, Vector3.up);
    }
    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    rigidBody.velocity = (player.transform.position - transform.position) *speed;
    //}
    void Update()
    {
        transform.position+= (player.transform.position - transform.position).normalized * speed*Time.deltaTime;
        playerPos.Set(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
