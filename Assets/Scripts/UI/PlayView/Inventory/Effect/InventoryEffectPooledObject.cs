using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class InventoryEffectPooledObject : UIAddEffectBase
{
    const int SCALE_VAIBRATE_AMOUNT = 1;

    [SerializeField]
    float _scalePunchAmount = 0.5f;
    [SerializeField]
    float _scalePunchTime = 0.4f;

    protected override void OnArrive()
    {
        List<int> a = new List<int> { 1, 2, 3, 4, 5 };
       
        image.transform.DOPunchScale(Vector3.one * _scalePunchAmount, _scalePunchTime, SCALE_VAIBRATE_AMOUNT).OnComplete(() => gameObject.SetActive(false));
    }
}
