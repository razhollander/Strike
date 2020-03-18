using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PowerUpgrader : UpgraderBase
{
    const string POWER_UPGRADE_NAME = "PowerUpgrade";
    protected override string UPGRADE_NAME => POWER_UPGRADE_NAME;

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Upgrade(UpgradeStockBase stockData)
    {
        base.Upgrade(stockData);
        PowerUpgradeStock powerStock = (PowerUpgradeStock)stockData;
        SetUpgradeValue(powerStock.Power);
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<float>();
    }


}
