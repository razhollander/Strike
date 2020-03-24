﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumsAmountUpgrader : UpgraderBase
{
    const string VACUUM_AMOUT_UPGRADE_NAME = "VacuumAmountUpgrade";
    protected override string UPGRADE_NAME => VACUUM_AMOUT_UPGRADE_NAME;

    public VacuumsAmountUpgrader(List<UpgradeStockBase> upgradeStocks) : base(upgradeStocks)
    {
        UpgradeType = eUpgradeType.VacuumsAmount;
    }

    public float GetUpgradeValue()
    {
        return base.GetUpgradeValue<float>();
    }

    public override void Upgrade()
    {
        base.Upgrade();
        VacuumsAmountUpgradeStock vacuumAmountStock = GetCurrentUpgradeStock<VacuumsAmountUpgradeStock>();
        SetUpgradeValue(vacuumAmountStock.VacuumsAmount);
    }

    public override void Reset()
    {
        SetUpgradeValue<float>(((VacuumsAmountUpgradeStock)UpgradeStocks[0]).VacuumsAmount);
    }
}