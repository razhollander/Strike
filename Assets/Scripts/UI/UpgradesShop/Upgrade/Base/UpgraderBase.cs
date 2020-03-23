﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UpgraderBase
{
    public eUpgradeType UpgradeType { get; protected set; }
    public event Action<int> OnUpgrade;
    private GameManager _gameManager;
    public List<UpgradeStockBase> UpgradeStocks { get; private set; }
    public UpgraderBase(List<UpgradeStockBase> upgradeStocks)
    {
        _gameManager = GameManager.Instance;
        UpgradeStocks = upgradeStocks;
        SetUpgradeDefault();
    }
    protected T GetUpgradeValue<T>()
    {
        return _gameManager.GamePrefHandler.LoadPref<T>(UPGRADE_NAME);
    }
    protected void SetUpgradeValue<T>(T value = default)
    {
        _gameManager.GamePrefHandler.SavePref<T>(value,UPGRADE_NAME);
    }
    protected void SetUpgradeDefault()
    {
        if (!_gameManager.GamePrefHandler.IsKeyExist(UPGRADE_NAME))
        {
            Reset();
        }
    }
    protected T GetCurrentUpgradeStock<T>() where T : UpgradeStockBase
    {
        return (T)UpgradeStocks[_gameManager.UpgradesManager.GetUpgradeLevel(UpgradeType)];
    }
    protected abstract string UPGRADE_NAME { get;}
    public virtual void Upgrade()
    {
        int currUpgradeLevel = _gameManager.UpgradesManager.GetUpgradeLevel(UpgradeType);
        currUpgradeLevel++;
        var stockData = UpgradeStocks[currUpgradeLevel];
        _gameManager.UpgradesManager.SetUpgradeLevel(UpgradeType, currUpgradeLevel);
        _gameManager.GameDataManager.Money -= stockData.Cost;
        OnUpgrade?.Invoke(currUpgradeLevel);
    }
    public abstract void Reset();

}
