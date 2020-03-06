using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrader : UpgraderBase
{
    const string SPEED_UPGRADE_NAME = "SpeedUpgrade";

    protected override string UPGRADE_NAME => SPEED_UPGRADE_NAME;

    public override void Upgrade(UpgradeStockBase stockData)
    {
        SpeedUpgradeStock speedStock = (SpeedUpgradeStock)stockData;
        SetUpgradeValue<float>(speedStock.Speed);
    }

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<float>();
    }
}
