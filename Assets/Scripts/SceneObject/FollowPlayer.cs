using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed=1;
    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<VehicleBehaviour.WheelVehicle>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (player.transform.position - transform.position)*speed;
    }
}
