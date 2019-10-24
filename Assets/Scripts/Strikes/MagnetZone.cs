using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetZone : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 magnetPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(magnetPos, radius);
        Rigidbody rb;
        foreach (Collider collider in colliders)
        {
            if (collider.transform.GetComponent<SuckableObject>() != null)
            {
                collider.transform.position += (transform.position - collider.transform.position).normalized * power;

                //rb = collider.transform.GetComponent<Rigidbody>();
                //if (rb != null)
                //rb.AddForce((transform.position - collider.transform.position).normalized*power, ForceMode.Force);
                //#if UNITY_EDITOR
                //else
                //    Debug.Log("rb NULL!");
                //#endif
            }

        }
        //StartCoroutine(DestroySelf());
    }
}
