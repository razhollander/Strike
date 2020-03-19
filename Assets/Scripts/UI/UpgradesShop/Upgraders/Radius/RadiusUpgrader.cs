using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusUpgrader : UpgraderBase
{
    const string RADIUS_UPGRADE_NAME = "RadiusUpgrade";
    protected override string UPGRADE_NAME => RADIUS_UPGRADE_NAME;

    public RadiusUpgrader(List<UpgradeStockBase> upgradeStocks) : base(upgradeStocks)
    {
        UpgradeType = eUpgradeType.Radius;
    }

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        RadiusUpgradeStock radiusStock = GetCurrentUpgradeStock<RadiusUpgradeStock>();
        SetUpgradeValue(radiusStock.Radius);
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<float>();
    }
    public override void Reset()
    {
        SetUpgradeValue<float>(((RadiusUpgradeStock)UpgradeStocks[0]).Radius);
    }

}
