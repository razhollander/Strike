using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradesShopView : MonoBehaviour
{
    [SerializeField] UpgradesShop _upgradesShop;
    [SerializeField] Transform _upgradesPanelsParent;
    [SerializeField] UpgradePanel _upgradePanel;
    [SerializeField] TextMeshProUGUI _moneyText;

    private void Start()
    {
        foreach (var upgradePanel in _upgradesShop.UpgradesPanelObjects)
        {
            var currUpgradePanel = Instantiate(_upgradePanel, _upgradesPanelsParent);
            currUpgradePanel.SetPanel(upgradePanel, GameManager.instance.UpgradesManager.GetUpgrade(upgradePanel.EUpgradeType));
        }
    }
}
