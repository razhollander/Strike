using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricStrike : BasicParticleStrike
{
    protected override float TimeToBeDestroyed
    {
        get
        {
            return particle.main.startLifetime.constantMax / particle.main.simulationSpeed + particle.main.startDelay.constant;
        }
    }


}
