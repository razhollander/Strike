using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckUpgradesAppearance : UpgradesAppearanceBase
{
    protected override void OnUpgrade(int level)
    {
        Destroy(transform.GetChild(0).gameObject);
        Transform newNeck = Instantiate(((SpeedUpgradeAppearanceObject)UpgradeAppearanceObject).Necks[level], transform).transform;
        newNeck.localPosition = Vector3.zero;
        newNeck.localRotation = Quaternion.identity;
        DoBuyEffect(newNeck);
    }
}
