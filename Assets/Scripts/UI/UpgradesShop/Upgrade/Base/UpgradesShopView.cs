﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpgradesShopView : MonoBehaviour
{
    public UpgradesShopObject UpgradesShopObject;

    [SerializeField] Transform _upgradesPanelsParent;
    [SerializeField] UpgradePanel _upgradePanel;
    GameManager _gm;

    private void Start()
    {
        _gm = GameManager.Instance;
        foreach (var upgradePanel in UpgradesShopObject.UpgradesPanelObjects)
        {
            var currUpgradePanel = Instantiate(_upgradePanel, _upgradesPanelsParent);
            currUpgradePanel.SetPanelInit(upgradePanel, _gm.UpgradesManager.GetUpgrade(upgradePanel.EUpgradeType));
        }
    }
    public void BackButtonPressed()
    {
        _gm.GameStateManager.SwitchGameState<MainMenuState>();
    }
    public void Reset()
    {
        _gm.UpgradesManager.Reset();
    }
}
