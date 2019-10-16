using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FirePinShot : BasicPinShot
{
    [Header("FirePinShot")]
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private float upForce;

    //[SerializeField] private float explosionRadius;

    protected override IEnumerator PinCollisionFunc(Enemy enemy)
    {
        enemy.SetHealth(-damage, true, true);
        rotationTweener.Kill();
        Explode();
        yield return null;
    }
    private void Explode()
    {
        Vector3 explosionPos = transform.position;
        //print("Explode");
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        //List<Rigidbody> rigidBodies = new List<Rigidbody>();
        Rigidbody rb;
        foreach (Collider collider in colliders)
        {
            //SuckableObject suckableObject = collider.transform.GetComponent<SuckableObject>()
            if (collider.transform.GetComponent<SuckableObject>() != null)
            {
               // if()
                rb = collider.transform.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upForce, ForceMode.Impulse);
#if UNITY_EDITOR
                else
                    Debug.Log("rb NULL!");
#endif
            }

        }
        //myRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        StartCoroutine(DestroySelf());

    }
}
