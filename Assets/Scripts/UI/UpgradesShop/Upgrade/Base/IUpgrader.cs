using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IUpgrader
{
    void Upgrade(int newUpgradeLevel, UpgradeStockBase stockData);
}
