using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicPinShot : ObjectShot
{
    protected Tween rotationTweener;

    // Start is called before the first frame update
    void Awake()
    {
        onCollisionFunc = PinCollisionFunc;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        DoSelfRotate();
    }
    protected void DoSelfRotate()
    {
        rotationTweener.Kill();
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    protected virtual IEnumerator PinCollisionFunc(Enemy enemy)
    {
        enemy.SetHealth(-damage, true, true);
        rotationTweener.Kill();
        StartCoroutine(DestroySelf());
        yield return null;
    }
}
