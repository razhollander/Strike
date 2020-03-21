using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntenaUpgradesAppearance : UpgradesAppearanceBase
{
    protected override void OnUpgrade(int level)
    {
        Destroy(transform.GetChild(0).gameObject);
        GameObject newVacuumHead = Instantiate(((RadiusUpgradeAppearanceObject)UpgradeAppearanceObject).Antenas[level], transform);
        newVacuumHead.transform.localPosition = Vector3.zero;
        newVacuumHead.transform.localRotation = Quaternion.identity;
    }
}
