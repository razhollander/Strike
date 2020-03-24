using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
public class Player : MonoBehaviour, ISceneObject
{
    [SerializeField] private float explosiveForce=100;
    [SerializeField] private float explosionRadius= 1000;
    [SerializeField] private float upForce;

    [SerializeField] public WheelVehicle WheelVehicle;
    [SerializeField] public GameObject Radius;

    public void DoQuitAnimation()
    {
        var rigidBodies =GameObject.FindObjectsOfType<Rigidbody>();
        Vector3 pos = transform.position;
        foreach (var rb in rigidBodies)
        {
            rb.AddExplosionForce(explosiveForce,pos, explosionRadius, upForce, ForceMode.Impulse);
        }
    }
}
