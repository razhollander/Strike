using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicPinShot : ObjectShot
{
    protected Tween rotationTweener;

    protected override void OnEnable()
    {
        base.OnEnable();
        DoSelfRotate();
    }
    protected void DoSelfRotate()
    {
        rotationTweener.Kill();
        rotationTweener = myRenderer.gameObject.transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    protected override void HandleCollision(Enemy enemy)
    {
        enemy.SetHealth(-damage, true, true);
        rotationTweener.Kill();
        base.HandleCollision(enemy);
    }
}
