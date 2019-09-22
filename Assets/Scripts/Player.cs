using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject car;
    [SerializeField]
    float rotationSpeed = 1;
    [SerializeField]
    float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        car.transform.Rotate(0, Input.GetAxis("Horizontal")* rotationSpeed*Time.deltaTime, 0);
        rb.AddForce(-car.transform.forward* speed* Input.GetAxis("Vertical"));

    }
}
