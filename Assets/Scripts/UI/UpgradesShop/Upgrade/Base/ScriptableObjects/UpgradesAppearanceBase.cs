using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradesAppearanceBase : MonoBehaviour
{
    [SerializeField]
    protected UpgradeAppearanceBaseObject UpgradeAppearanceObject;
    protected abstract void OnUpgrade(int level); 
    void Start()
    {
        InitAppearance();
        GameManager.Instance.UpgradesManager.GetUpgrade(UpgradeAppearanceObject.UpgradeType).OnUpgrade += OnUpgrade;
    }
    protected virtual void InitAppearance()
    {
        OnUpgrade(GameManager.Instance.UpgradesManager.GetUpgradeLevel(UpgradeAppearanceObject.UpgradeType));
    }
}
