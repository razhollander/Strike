using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class VacuumButton : MonoBehaviour
{
    [SerializeField] private float _shakeStrength= 90;
    [SerializeField] private int _shakeVibration = 10;
    [SerializeField] private float _shakeRandomness = 10;
    [SerializeField] private bool _isFadeOut = false;
    [SerializeField] private Mask _mask;
    Tween _shakeTweener;

    //private void OnEnable()
    //{
    //    _mask.enabled = true;
    //}
    //private void OnDisable()
    //{
    //    _mask.enabled = false;
    //}
    public void StartShakeEffect()
    {
        if (_shakeTweener == null)
        {
            _shakeTweener = transform.DOShakeRotation(10, _shakeStrength, _shakeVibration, _shakeRandomness, _isFadeOut).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
    public void StopShakeEffect()
    {
        _shakeTweener.OnKill(SetShakeTweenerNull);
        _shakeTweener.Kill();
        transform.localRotation = Quaternion.identity;
    }
    private void SetShakeTweenerNull()
    {
        _shakeTweener = null;
    }
}
