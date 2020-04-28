using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopController
{

    private UpgradesShopView _upgradesShopView;
    private UpgradesShopModel _dataModel { get; set; }

    public UpgradesShopController(UpgradesShopModel upgradesShopModel, UpgradesShopView upgradesShopView)
    {
        _dataModel = upgradesShopModel;
        _upgradesShopView = upgradesShopView;
        //GameManager.Instance.GameDataManager.OnPlayerMoneyChanged += _upgradesShopView.UpdateMoneyText;
    }


}
