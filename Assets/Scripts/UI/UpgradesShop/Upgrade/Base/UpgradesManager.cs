using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager
{
    // Start is called before the first frame update
    Dictionary<eUpgradeType,IUpgrader> Upgraders;

    public UpgradesManager()
    {
        Upgraders = new Dictionary<eUpgradeType, IUpgrader>();
        //var upgraders = Object.FindObjectsOfTypeIncludingAssets(typeof(IUpgrader));
        //Debug.Log(upgraders.Length);
        Upgraders.Add(eUpgradeType.Power, new PowerUpgrader());
        Upgraders.Add(eUpgradeType.Speed, new PowerUpgrader());
        Upgraders.Add(eUpgradeType.Vacuums, new PowerUpgrader());
    }
    public IUpgrader GetUpgrade(eUpgradeType upgradeType)
    {
        return Upgraders[upgradeType];
    }

}
