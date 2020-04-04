using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MagneticPinShot : BasicPinShot
{
//    [Header("MagneticPinShot")]


//    protected override IEnumerator PinCollisionFunc(Enemy enemy)
//    {
//        enemy.SetHealth(-damage, true, true);
//        rotationTweener.Kill();
//        Explode();
//        yield return null;
//    }

//    private void Explode()
//    {
//        Vector3 explosionPos = transform.position;
//        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
//        Rigidbody rb;
//        foreach (Collider collider in colliders)
//        {
//            if (collider.transform.GetComponent<SuckableObject>() != null)
//            {
//                rb = collider.transform.GetComponent<Rigidbody>();
//                if (rb != null)
//                    rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upForce, ForceMode.Impulse);
//#if UNITY_EDITOR
//                else
//                    Debug.Log("rb NULL!");
//#endif
//            }

//        }
//        CameraHandler.instance.ShakeCamera();
//        StartCoroutine(DestroySelf());

//    }
}
