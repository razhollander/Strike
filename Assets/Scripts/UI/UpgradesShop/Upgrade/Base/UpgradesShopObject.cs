using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesShopObject", menuName = "Upgrades/UpgradeShop")]
public class UpgradesShopObject : ScriptableObject
{
    public List<UpgradesPanelObject> UpgradesPanelObjects;
}
