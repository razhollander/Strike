﻿using UnityEngine;
using System.Collections;

public class ParticleToTarget : OverridableMonoBehaviour
{
    public Transform Target;
    public float speed = 1;
    private ParticleSystem system;

    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

    int count;

    protected override void Awake()
    {
        base.Awake();
        if (system == null)
            system = GetComponent<ParticleSystem>();

        if (system == null)
        {
            this.enabled = false;
        }

    }

    public override void UpdateMe()
    {

        count = system.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            ParticleSystem.Particle particle = particles[i];

            Vector3 v1 = system.transform.TransformPoint(particle.position);
            Vector3 v2 = Target.transform.position;

            Vector3 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime)* speed;
            particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
            particles[i] = particle;
        }

        system.SetParticles(particles, count);
    }
}