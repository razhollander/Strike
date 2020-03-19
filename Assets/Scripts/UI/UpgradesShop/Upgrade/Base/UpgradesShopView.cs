using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpgradesShopView : MonoBehaviour
{
    public UpgradesShopObject UpgradesShopObject;

    [SerializeField] Transform _upgradesPanelsParent;
    [SerializeField] UpgradePanel _upgradePanel;
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Start()
    {
        foreach (var upgradePanel in UpgradesShopObject.UpgradesPanelObjects)
        {
            var currUpgradePanel = Instantiate(_upgradePanel, _upgradesPanelsParent);
            currUpgradePanel.SetPanelInit(upgradePanel, GameManager.Instance.UpgradesManager.GetUpgrade(upgradePanel.EUpgradeType));
        }

        _moneyText.text = GameManager.Instance.GameDataManager.Money.ToString();
    }
    public void UpdateMoneyText(int newPlayerMoney)
    {
        _moneyText.text = newPlayerMoney.ToString();
    }
    public void BackButtonPressed()
    {
        GameManager.Instance.GameStateManager.SwitchGameState(eGameState.MainMenu);
    }
    public void Reset()
    {
        GameManager.Instance.UpgradesManager.Reset();
    }
}
