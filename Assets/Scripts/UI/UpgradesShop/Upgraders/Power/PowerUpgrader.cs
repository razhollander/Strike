using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerUpgrader : UpgraderBase
{
    const string POWER_UPGRADE_NAME = "PowerUpgrade";
    protected override string UPGRADE_NAME => POWER_UPGRADE_NAME;

    public PowerUpgrader(List<UpgradeStockBase> upgradeStocks) : base (upgradeStocks)
    {
        UpgradeType = eUpgradeType.Power;
    }

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        PowerUpgradeStock powerStock = GetCurrentUpgradeStock<PowerUpgradeStock>();
        SetUpgradeValue(powerStock.Power);
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<float>();
    }
    public override void Reset()
    {
        SetUpgradeValue<float>(((PowerUpgradeStock)UpgradeStocks[0]).Power);
    }

}
