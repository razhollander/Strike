using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusUpgrader : UpgraderBase
{
    const string RADIUS_UPGRADE_NAME = "RadiusUpgrade";

    protected override string UPGRADE_NAME => RADIUS_UPGRADE_NAME;

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Upgrade(UpgradeStockBase stockData)
    {
        RadiusUpgradeStock radiusStock = (RadiusUpgradeStock)stockData;
        SetUpgradeValue(radiusStock.Radius);
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<float>();
    }
}
