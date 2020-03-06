using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UpgraderBase
{
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
    public abstract void Upgrade(UpgradeStockBase stockData);

}
