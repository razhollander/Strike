using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntenaUpgradesAppearance : UpgradesAppearanceBase
{
    protected override void OnUpgrade(int level)
    {
        Destroy(transform.GetChild(0).gameObject);
        Transform newAntena = Instantiate(((RadiusUpgradeAppearanceObject)UpgradeAppearanceObject).Antenas[level], transform).transform;
        newAntena.localPosition = Vector3.zero;
        newAntena.localRotation = Quaternion.identity;
        DoBuyEffect(newAntena);
    }
}
