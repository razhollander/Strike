using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesShop", menuName = "Upgrades/UpgradeShop")]
public class UpgradesShop : ScriptableObject
{
    public List<UpgradesPanelObject> UpgradesPanelObjects;
}
