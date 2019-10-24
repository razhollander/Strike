using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricStrike : PooledMonobehaviour
{
    public ParticleSystem lightningParticle;
    [SerializeField] private float duration;

    private void OnEnable()
    {
        StartCoroutine(DisableThis());
    }
    private IEnumerator DisableThis()
    {
        yield return null;
        yield return new WaitForSeconds(lightningParticle.main.startLifetime.constantMax/ lightningParticle.main.simulationSpeed+ lightningParticle.main.startDelay.constant);
        lightningParticle.Stop();
        var main = lightningParticle.main;
        main.startDelay = 0;
        gameObject.SetActive(false);
    }

}   
