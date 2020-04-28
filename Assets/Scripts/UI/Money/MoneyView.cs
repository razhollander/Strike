using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyView : Countable
{
    protected override void OnEnable()
    {
        if (GameManager.Instance != null && GameManager.Instance.MoneyModel != null)
        {
            SetStartingValue(GameManager.Instance.MoneyModel.Money);
            GameManager.Instance.MoneyModel.OnPlayerMoneyChanged += SetNumber;
        }
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null && GameManager.Instance.MoneyModel != null)
        {
            GameManager.Instance.MoneyModel.OnPlayerMoneyChanged -= SetNumber;
        }
    }
}
