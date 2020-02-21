using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopView : MonoBehaviour
{
    [SerializeField] UpgradesShop _upgradesShop;
    [SerializeField] Transform _upgradesPanelsParent;
    [SerializeField] UpgradePanel _upgradePanel;

    void Start()
    {
        foreach (var upgradePanel in _upgradesShop.UpgradesPanelObjects)
        {
            var currUpgradePanel = Instantiate(_upgradePanel, _upgradesPanelsParent);
            currUpgradePanel.Populate(upgradePanel, GameManager.instance.UpgradesManager.GetUpgrade(upgradePanel.EUpgradeType));
        }   
    }
}
