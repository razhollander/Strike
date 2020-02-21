using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "UpgradesPanel", menuName = "Upgrades/UpgradesPanel")]
public class UpgradesPanelObject : ScriptableObject
{
    public int CurrentUpgradeLevel;
    public List<UpgradeStockBase> UpgradeStocks;
    public eUpgradeType EUpgradeType;
}

public enum eUpgradeType
{
    Power,
    Speed,
    Vacuums
}
