using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UpgraderBase
{
    public eUpgradeType UpgradeType { get; protected set; }
    public event Action<int> OnUpgrade;

    public UpgraderBase()
    {
        SetUpgradeDefault();
    }
    protected T GetUpgradeValue<T>()
    {
        return GameManager.Instance.GamePrefHandler.LoadPref<T>(UPGRADE_NAME);
    }
    protected void SetUpgradeValue<T>(T value = default)
    {
        GameManager.Instance.GamePrefHandler.SavePref<T>(value,UPGRADE_NAME);
    }
    protected void SetUpgradeDefault<T>()
    {
        if (!GameManager.Instance.GamePrefHandler.IsKeyExist(UPGRADE_NAME))
        {
            SetUpgradeValue<T>();
        }
    }
    protected abstract string UPGRADE_NAME { get;}
    protected abstract void SetUpgradeDefault();
    public virtual void Upgrade(UpgradeStockBase stockData)
    {
        int currUpgradeLevel = GameManager.Instance.UpgradesManager.GetUpgradeLevel(UpgradeType);
        currUpgradeLevel++;
        GameManager.Instance.UpgradesManager.SetUpgradeLevel(UpgradeType, currUpgradeLevel);
        GameManager.Instance.GameDataManager.Money -= stockData.Cost;
        OnUpgrade?.Invoke(currUpgradeLevel);
    }
    //public void UpgradeNoCost(UpgradeStockBase stockData, int currUpgradeLevel)
    //{
    //    currUpgradeLevel++;
    //    GameManager.Instance.UpgradesManager.SetUpgradeLevel(UpgradeType, currUpgradeLevel);
    //}

}
