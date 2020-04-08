using UnityEngine;
using System.Collections;
public class ParticleToTarget : OverridableMonoBehaviour
{
    public Transform Target;
    public float speed = 1;
    private ParticleSystem system;
    Vector3 v2;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
    [SerializeField] bool _isWorldSpace = false;
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
        if (Target != null)
        {
            v2 = Target.position;
        }

        count = system.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            if (Target != null)
            {
                ParticleSystem.Particle particle = particles[i];

                Vector3 v1;
                if (!_isWorldSpace)
                    v1 = system.transform.TransformPoint(particle.position);
                else
                    v1 = particle.position;

                Vector3 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime) * speed * Time.deltaTime;
                if (!_isWorldSpace)
                    particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                else
                    particle.position = v2 - tarPosi;
                particles[i] = particle;
            }

        }

        system.SetParticles(particles, count);
    }
}