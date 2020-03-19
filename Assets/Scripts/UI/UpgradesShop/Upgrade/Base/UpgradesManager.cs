using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UpgradesManager
{
    private const string UPGRADES_SHOP_VIEW_NAME = "UpgradesShopView";

    private List<UpgraderBase> _upgraders;
    private UpgradesShopController _upgradesShopController;
    private UpgradesShopModel _upgradesShopModel;
    private UpgradesShopView _upgradesShopView;
    private UpgradesShopObject _upgradesShopObject;

    public UpgradesManager()
    {
        _upgradesShopModel = new UpgradesShopModel();
        _upgradesShopView = GameManager.Instance.AssetLoadHandler.LoadAsset<UpgradesShopView>(UPGRADES_SHOP_VIEW_NAME);
        _upgradesShopController = new UpgradesShopController(_upgradesShopModel, _upgradesShopView);
        _upgradesShopObject = _upgradesShopView.UpgradesShopObject;
        _upgraders = new List<UpgraderBase>()
        {
             new PowerUpgrader(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType == eUpgradeType.Power).UpgradeStocks),
             new SpeedUpgrader(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType == eUpgradeType.Speed).UpgradeStocks),
             new VacuumsAmountUpgrader(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType == eUpgradeType.VacuumsAmount).UpgradeStocks),
             new RadiusUpgrader(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType == eUpgradeType.Radius).UpgradeStocks)
        };
    }
    public T GetUpgrade<T>() where T : UpgraderBase
    {
        return (T)_upgraders.FirstOrDefault(x => x.GetType() == typeof(T));
    }
    public UpgraderBase GetUpgrade(eUpgradeType upgradeType)
    {
        return _upgraders.FirstOrDefault(x => x.UpgradeType == upgradeType);
    }
    public int GetUpgradeLevel(eUpgradeType upgradeType)
    {
        return _upgradesShopModel.GetUpgradeLevel(upgradeType);
    }
    public void SetUpgradeLevel(eUpgradeType upgradeType, int level)
    {
        _upgradesShopModel.SetUpgradeLevel(upgradeType, level);
    }
    public void Reset()
    {
        foreach (eUpgradeType upgradeType in (eUpgradeType[])Enum.GetValues(typeof(eUpgradeType)))
        {
            GameManager.Instance.UpgradesManager.SetUpgradeLevel(upgradeType, 0);
        }
        foreach (var upgrader in _upgraders)
        {
            upgrader.Reset();
        }
    }
}
public enum eUpgradeType
{
    Power = 0,
    Speed = 1,
    VacuumsAmount = 2,
    Radius = 3
}