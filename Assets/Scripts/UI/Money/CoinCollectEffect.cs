using DG.Tweening;
using UnityEngine;

public class CoinCollectEffect : UIAddEffectBase
{
    [Header("Coin Collect Effect")]

    const int SCALE_VAIBRATE_AMOUNT = 1;

    [SerializeField] 
    ParticleSystem _onCollectedCircleEffect;
    [SerializeField]
    float _scalePunchAmount = 0.7f;
    [SerializeField]
    float _scalePunchTime = 0.4f;

    protected override void OnArrive()
    {
        _onCollectedCircleEffect.Play();
        image.transform.DOPunchScale(Vector3.one * _scalePunchAmount, _scalePunchTime, SCALE_VAIBRATE_AMOUNT).OnComplete(() => gameObject.SetActive(false));
    }

}
