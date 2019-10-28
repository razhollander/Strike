using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicParticleStrike : PooledMonobehaviour
{
    public ParticleSystem particle;
    float timeToBeDestroyed = 1;
    protected virtual float TimeToBeDestroyed { get { return timeToBeDestroyed; }}

    private void OnEnable()
    {
        StartCoroutine(DisableThis());
    }
    private IEnumerator DisableThis()
    {
        yield return null;
        yield return new WaitForSeconds(TimeToBeDestroyed);
        //yield return new WaitForSeconds(2);
        particle.Stop();
        var main = particle.main;
        main.startDelay = 0;
        gameObject.SetActive(false);
    }
}
