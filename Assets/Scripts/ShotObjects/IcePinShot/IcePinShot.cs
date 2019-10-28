using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IcePinShot : BasicPinShot
{
    [Header("Ice Shot")]

    [SerializeField] ConeHitBox hitBox;
    //[SerializeField] ParticleSystem IceSparks;
    [SerializeField] IceStrike iceStrike;
    List<Enemy> enemiesList;
    static Vector3 hitBoxSize;


    protected override IEnumerator PinCollisionFunc(Enemy enemy)
    {
        rotationTweener.Kill();
        IceStrike();
        yield return null;
    }
    private void IceStrike()
    {
        foreach (Enemy enemy in hitBox.GetEnemiesInBounds())
        {
            enemy.SetHealth(-damage, true, true);
            IceStrike currIceStrike = iceStrike.Get<IceStrike>(true);
            currIceStrike.transform.position = enemy.transform.position;
            currIceStrike.particle.Play();
        }
        //do Global FX
        //do FX Foreach Pin
        //each pin get hit
        StartCoroutine(DestroySelf());
    }

}
