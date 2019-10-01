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
    [SerializeField]
    float maxSpeed = 10;

    public Transform VaccumPoint;
    public float rotation = 0;
    public float driftPower = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        car.transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);
        if(rb.velocity.magnitude< maxSpeed)
        rb.AddForce(-car.transform.forward * speed /** Input.GetAxis("Vertical")*/);
    }
}
