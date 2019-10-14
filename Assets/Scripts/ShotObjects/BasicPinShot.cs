using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicPinShot : ObjectShot
{
    Tween rotationTweener;

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
        rotationTweener = transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }
    private IEnumerator PinCollisionFunc(Enemy enemy)
    {
        enemy.SetHealth(-damage, true, true);
        StartCoroutine(DestroySelf());
        yield return null;
    }
}
