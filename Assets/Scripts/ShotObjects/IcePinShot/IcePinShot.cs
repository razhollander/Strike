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
    [SerializeField] float forceHitPower;
    List<Enemy> enemiesList;
    static Vector3 hitBoxSize;

    protected override void HandleCollision(Enemy enemy, Vector3 collisionPoint)
    {
        rotationTweener.Kill();
        IceStrike();
    }
    private void IceStrike()
    {
        Vector3 force = myRigidbody.velocity.normalized * forceHitPower;
        foreach (Enemy enemy in hitBox.GetEnemiesInBounds())
        {
            enemy.SetHealth(-damage, true, true);
            enemy.AddForce(force);
            IceStrike currIceStrike = iceStrike.Get<IceStrike>(true);
            currIceStrike.transform.position = enemy.transform.position;
            currIceStrike.transform.SetParent(enemy.transform);
            currIceStrike.particle.Play();
        }
        CameraHandler.instance.ShakeCamera(0.5f, 0.2f);
        StartCoroutine(DestroySelf());
    }

}
