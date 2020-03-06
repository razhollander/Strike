using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager
{
    private const string UPGRADES_SHOP_VIEW_NAME = "UpgradesShopView";

    private Dictionary<eUpgradeType,UpgraderBase> _upgraders;
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
        _upgraders = new Dictionary<eUpgradeType, UpgraderBase>();
        _upgraders.Add(eUpgradeType.Power, new PowerUpgrader());
        _upgraders.Add(eUpgradeType.Speed, new SpeedUpgrader());
        _upgraders.Add(eUpgradeType.VacuumsAmount, new VacuumsAmountUpgrader());
        _upgraders.Add(eUpgradeType.Radius, new RadiusUpgrader());

        OnEnableUpgrade();
    }
    private void OnEnableUpgrade()
    {
        foreach (var upgrader in _upgraders)
        {
            for (int i = 0; i < GetUpgradeLevel(upgrader.Key); i++)
            {
                upgrader.Value.Upgrade(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType==upgrader.Key).UpgradeStocks[i]);
            }
        }
    }
    public T GetUpgrade<T>(eUpgradeType upgradeType) where T : UpgraderBase
    {
        return (T)_upgraders[upgradeType];
    }
    public UpgraderBase GetUpgrade(eUpgradeType upgradeType)
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
public enum eUpgradeType
{
    Power = 0,
    Speed = 1,
    VacuumsAmount = 2,
    Radius = 3
}