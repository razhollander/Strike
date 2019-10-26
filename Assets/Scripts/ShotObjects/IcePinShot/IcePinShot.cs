using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IcePinShot : BasicPinShot
{
    [SerializeField] Collider hitBox;
    //protected override IEnumerator PinCollisionFunc(Enemy enemy)
    //{
    //    enemy.SetHealth(-damage, true, true);
    //    rotationTweener.Kill();
    //    IceStrike();
    //    yield return null;
    //}
    private void IceStrike()
    {

    }
}
