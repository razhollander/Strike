using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
        _upgraders = new List<UpgraderBase>();
        _upgraders.Add(new PowerUpgrader());
        _upgraders.Add(new SpeedUpgrader());
        _upgraders.Add(new VacuumsAmountUpgrader());
        _upgraders.Add(new RadiusUpgrader());

       // OnEnableUpgrade();
    }
    //private void OnEnableUpgrade()
    //{
    //    foreach (var upgrader in _upgraders)
    //    {
    //        upgrader.UpgradeNoCost(_upgradesShopObject.UpgradesPanelObjects.Find(x=>x.EUpgradeType==upgrader.UpgradeType).UpgradeStocks[i], GetUpgradeLevel(upgrader.UpgradeType));
    //    }
    //}
    public T GetUpgrade<T>() where T : UpgraderBase
    {
        return (T)_upgraders.FirstOrDefault(x => x.GetType() ==  typeof(T));
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

}
public enum eUpgradeType
{
    Power = 0,
    Speed = 1,
    VacuumsAmount = 2,
    Radius = 3
}