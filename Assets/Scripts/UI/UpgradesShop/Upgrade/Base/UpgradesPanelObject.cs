using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UpgradesPanel", menuName = "Upgrades/UpgradesPanel")]
public class UpgradesPanelObject : ScriptableObject
{
    public string Name;
    public List<UpgradeStockBase> UpgradeStocks;
    public eUpgradeType EUpgradeType;
}