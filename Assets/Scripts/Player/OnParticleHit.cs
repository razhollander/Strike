using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleHit : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit");
    }

}
