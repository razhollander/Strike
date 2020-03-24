using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumUpgradesAppearance : UpgradesAppearanceBase
{
    protected override void OnUpgrade(int level)
    {
        Destroy(transform.GetChild(0).gameObject);
        Transform newVacuumHead = Instantiate(((PowerUpgradeAppearanceObject)UpgradeAppearanceObject).VacuumsHeads[level], transform).transform;
        newVacuumHead.localPosition = Vector3.zero;
        newVacuumHead.localRotation = Quaternion.identity;
        DoBuyEffect(newVacuumHead.transform);

    }
}
