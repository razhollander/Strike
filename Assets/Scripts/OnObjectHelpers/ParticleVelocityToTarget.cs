using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem ))]
public class ParticleVelocityToTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] ParticleSystem _particleSystem;
    float velocity;
    float worldSpaceScale;
    // Start is called before the first frame update
    void Awake()
    {
        ParticleSystem.VelocityOverLifetimeModule vel = _particleSystem.velocityOverLifetime;
        velocity = Vector3.Magnitude(new Vector3(vel.xMultiplier, vel.yMultiplier, vel.zMultiplier));
        Debug.Log(new Vector3(vel.xMultiplier, vel.yMultiplier, vel.zMultiplier));
        worldSpaceScale = transform.localToWorldMatrix.lossyScale.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        

        //Vector3 dirOne = dir.normalized;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[_particleSystem.particleCount];
        int count = _particleSystem.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            Vector3 dir = target.position - _particleSystem.transform.TransformPoint(particles[i].position);
            //float yVel = (particles[i].remainingLifetime / particles[i].startLifetime) * dir.magnitude;
            //particles[i].velocity = new Vector3(0, yVel, 0);
            particles[i].remainingLifetime = dir.magnitude/ velocity;
            //Debug.Log(dir.magnitude +" "+ velocity+ " "+particles[i].remainingLifetime);
            if ((target.position -transform.position).sqrMagnitude * worldSpaceScale < (_particleSystem.transform.TransformPoint(particles[i].position) - transform.position).sqrMagnitude)
            {
                particles[i].remainingLifetime=0;
            }
        }

        _particleSystem.SetParticles(particles, count);


    }
}
