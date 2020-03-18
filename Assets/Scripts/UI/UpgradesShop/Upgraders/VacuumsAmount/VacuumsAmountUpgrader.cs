using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumsAmountUpgrader : UpgraderBase
{
    const string VACUUM_AMOUT_UPGRADE_NAME = "VacuumAmountUpgrade";
    protected override string UPGRADE_NAME => VACUUM_AMOUT_UPGRADE_NAME;

    public int GetUpgradeValue()
    {
        return base.GetUpgradeValue<int>();
    }

    public override void Upgrade(UpgradeStockBase stockData)
    {
        base.Upgrade(stockData);
        VacuumsAmountUpgradeStock vacuumAmountStock = (VacuumsAmountUpgradeStock)stockData;
        SetUpgradeValue(vacuumAmountStock.VacuumsAmount);
    }

    protected override void SetUpgradeDefault()
    {
        SetUpgradeDefault<int>();
    }
}
