using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadiusUpgradeAppearance", menuName = "Upgrades/Appearance/RadiusAppearance")]
public class RadiusUpgradeAppearanceObject : UpgradeAppearanceBaseObject
{
    public List<RadiusAppearanceStock> radiusAppearanceStocks;
}
[System.Serializable]
public class RadiusAppearanceStock
{
    public int size;
    public Material mat;
}
