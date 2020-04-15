using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBowlingBallStrike : BasicParticleStrike
{
    protected override float TimeToBeDestroyed
    {
        get
        {
            return particle.main.duration / particle.main.simulationSpeed;
        }
    }
}
