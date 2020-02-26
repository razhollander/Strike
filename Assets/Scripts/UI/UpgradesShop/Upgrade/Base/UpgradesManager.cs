using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager
{
    private Dictionary<eUpgradeType,IUpgrader> _upgraders;
    private UpgradesShopController _upgradesShopController;
    private UpgradesShopModel _upgradesShopModel;

    public UpgradesManager()
    {
        _upgradesShopModel = new UpgradesShopModel();
        _upgradesShopController = new UpgradesShopController(_upgradesShopModel);
        _upgraders = new Dictionary<eUpgradeType, IUpgrader>();
        _upgraders.Add(eUpgradeType.Power, new PowerUpgrader());
        _upgraders.Add(eUpgradeType.Speed, new SpeedUpgrader());
    }
    public IUpgrader GetUpgrade(eUpgradeType upgradeType)
    {
        return _upgraders[upgradeType];
    }
    public int GetUpgradeLevel(eUpgradeType upgradeType)
    {
        return _upgradesShopModel.GetUpgradeLevel(upgradeType);
    }
    public void SetUpgradeLevel(eUpgradeType upgradeType, int level)
    {
        _upgradesShopModel.SetUpgradeLevel(upgradeType, level);
    }

}
