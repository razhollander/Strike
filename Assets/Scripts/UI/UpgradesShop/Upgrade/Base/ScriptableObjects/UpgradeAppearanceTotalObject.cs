using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeAppearanceTotalObject", menuName = "Upgrades/Appearance/UpgradeAppearanceTotal")]
public class UpgradeAppearanceTotalObject : ScriptableObject
{
    public List<UpgradeAppearanceBaseObject> upgradeAppearanceBaseObjects;
}
