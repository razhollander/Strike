using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrader : UpgraderBase
{
    const string SPEED_UPGRADE_NAME = "SpeedUpgrade";

    protected override string UPGRADE_NAME => SPEED_UPGRADE_NAME;

    public SpeedUpgrader(List<UpgradeStockBase> upgradeStocks) : base (upgradeStocks)
    {
        UpgradeType = eUpgradeType.Speed;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        SpeedUpgradeStock speedStock = GetCurrentUpgradeStock<SpeedUpgradeStock>();
        SetUpgradeValue<float>(speedStock.Speed);
    }

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Reset()
    {
        SetUpgradeValue<float>(((SpeedUpgradeStock)UpgradeStocks[0]).Speed);
    }
}
