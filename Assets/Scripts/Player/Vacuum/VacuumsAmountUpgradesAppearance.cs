using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumsAmountUpgradesAppearance : UpgradesAppearanceBase
{
    const int UPGRADES_PER_VACUUM = 3;
    [SerializeField] private Vacuum[] _vacuumsArr;
    protected override void OnUpgrade(int level)
    {
        if (level > 0)
        {
            int appearanceLevel = level - 1;
            Vacuum currVacuumBeingUpdated = _vacuumsArr[(appearanceLevel + UPGRADES_PER_VACUUM )/ UPGRADES_PER_VACUUM];
            float size = ((VacuumsAmoutUpgradeAppearanceObject)UpgradeAppearanceObject).VacuumSize[level];
            currVacuumBeingUpdated.transform.parent.localScale = Vector3.one * size;

            if (appearanceLevel % UPGRADES_PER_VACUUM == 0)
            {
                currVacuumBeingUpdated.gameObject.SetActive(true);
            }
        }
    }
    protected override void InitAppearance()
    {
        for (int i = 0; i < _vacuumsArr.Length; i++)
        {
            _vacuumsArr[i].VacuumNumber = i;
        }

        for (int i = 1; i <= GameManager.Instance.UpgradesManager.GetUpgradeLevel(eUpgradeType.VacuumsAmount); i++)
        {
            OnUpgrade(i);
        }
    }
}
