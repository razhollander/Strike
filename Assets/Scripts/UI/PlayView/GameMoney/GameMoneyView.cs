using UnityEngine;
using UnityEngine.UI;
public class GameMoneyView : Countable
{
    [SerializeField] Image _coinImage;
    [SerializeField] CoinCollectEffect _coinEffect;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
    }

    public void StartCoinAddEffect(int moneyValue, Vector3 startPos)
    {
        _coinEffect.Get<CoinCollectEffect>(transform).StartEffect(_coinImage.sprite, _rectTransform, startPos,()=> SetNumber(moneyValue));
    }
}
