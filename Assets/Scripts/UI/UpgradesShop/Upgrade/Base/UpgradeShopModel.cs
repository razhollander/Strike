using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopModel
{
    const string UPGRADES_LEVELS = "UpgradesLevel";

    Dictionary<eUpgradeType, int> _upgradesLevelsDictionary;
   
    public UpgradesShopModel()
    {
        if (!GameManager.Instance.GamePrefHandler.IsKeyExist(UPGRADES_LEVELS))
        {
            _upgradesLevelsDictionary = new Dictionary<eUpgradeType, int>();
        }
        else
        {
            _upgradesLevelsDictionary = GameManager.Instance.GamePrefHandler.LoadPref<Dictionary<eUpgradeType, int>>(UPGRADES_LEVELS);
        }

        foreach (eUpgradeType upgradeType in (eUpgradeType[])Enum.GetValues(typeof(eUpgradeType)))
        {
            if (!_upgradesLevelsDictionary.ContainsKey(upgradeType))
            {
                _upgradesLevelsDictionary.Add(upgradeType, 0);
            }

            SetUpgradeLevel(upgradeType, _upgradesLevelsDictionary[upgradeType]);
        }
    }

    public int GetUpgradeLevel(eUpgradeType upgradeType)
    {
        return _upgradesLevelsDictionary[upgradeType];
    }
    public void SetUpgradeLevel(eUpgradeType upgradeType, int level)
    {
        _upgradesLevelsDictionary[upgradeType] = level;
        GameManager.Instance.GamePrefHandler.SavePref<Dictionary<eUpgradeType, int>>(_upgradesLevelsDictionary, UPGRADES_LEVELS);
    }
}
