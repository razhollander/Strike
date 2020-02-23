using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopModel
{
    const string PLAYER_MONEY = "PlayerMoney";
    const string UPGRADES_LEVELS = "UpgradesLevel";

    Dictionary<eUpgradeType, int> _upgradesLevelsDictionary;

    public event Action<int> OnMoneyChanged;
    public event Action<int> OnPowerChanged;

    ES3Settings settings;
    public UpgradesShopModel()
    {
        settings = new ES3Settings(ES3.EncryptionType.AES, "razboost");
        settings.location = ES3.Location.PlayerPrefs;

        if (!ES3.KeyExists(UPGRADES_LEVELS))
        {
            _upgradesLevelsDictionary = new Dictionary<eUpgradeType, int> {
                { eUpgradeType.Power,0 },
                 { eUpgradeType.Speed,0 },
                  { eUpgradeType.Vacuums,0 }
            };

            ES3.Save<Dictionary<eUpgradeType, int>>(UPGRADES_LEVELS, _upgradesLevelsDictionary, settings);
        }
        else
        {
            _upgradesLevelsDictionary = ES3.Load<Dictionary<eUpgradeType, int>>(UPGRADES_LEVELS, settings);
        }

        if (!ES3.KeyExists(PLAYER_MONEY))
        {
            ES3.Save<int>(PLAYER_MONEY, 0, settings);
        }
    }

    public int GetUpgradeLevel(eUpgradeType upgradeType)
    {
        return _upgradesLevelsDictionary[upgradeType];
    }
    public void SetUpgradeLevel(eUpgradeType upgradeType, int level)
    {
        _upgradesLevelsDictionary[upgradeType] = level;
        ES3.Save<Dictionary<eUpgradeType, int>>(UPGRADES_LEVELS, _upgradesLevelsDictionary, settings);
    }

    public int PlayerMoney
    {
        get
        {
            return ES3.Load<int>(PLAYER_MONEY, settings);
        }
        set
        {
            ES3.Save<int>(PLAYER_MONEY, value, settings);
            OnMoneyChanged(value);
        }
    }

}
