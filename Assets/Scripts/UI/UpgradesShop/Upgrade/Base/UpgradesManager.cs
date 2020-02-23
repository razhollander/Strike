using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager
{
    private Dictionary<eUpgradeType,IUpgrader> _upgraders;
    public UpgradesManager(UpgradesShopView upgradesShopView)
    {
        _upgraders = new Dictionary<eUpgradeType, IUpgrader>();
        _upgraders.Add(eUpgradeType.Power, new PowerUpgrader());
        _upgraders.Add(eUpgradeType.Speed, new SpeedUpgrader());
    }
    public IUpgrader GetUpgrade(eUpgradeType upgradeType)
    {
        return _upgraders[upgradeType];
    }

}
