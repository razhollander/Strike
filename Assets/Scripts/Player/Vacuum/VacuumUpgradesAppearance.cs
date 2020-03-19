using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumUpgradesAppearance : MonoBehaviour
{
    [SerializeField]
    private PowerUpgradeAppearanceObject powerUpgradeAppearanceObject;
    [SerializeField]
    private GameObject vacuumHead;
    // Start is called before the first frame update
    void Start()
    {
        InitAppearance();
        GameManager.Instance.UpgradesManager.GetUpgrade(eUpgradeType.Power).OnUpgrade += OnPowerUpgrade;
    }
    void OnPowerUpgrade(int level)
    {
        Destroy(vacuumHead.transform.GetChild(0).gameObject);
        GameObject newVacuumHead = Instantiate(powerUpgradeAppearanceObject.VacuumsHeads[level], vacuumHead.transform);
        newVacuumHead.transform.localPosition = Vector3.zero;
        newVacuumHead.transform.localRotation = Quaternion.identity;
    }
    void InitAppearance()
    {
        //Power
        OnPowerUpgrade(GameManager.Instance.UpgradesManager.GetUpgradeLevel(eUpgradeType.Power));
    }

}
