using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopController
{
    // Start is called before the first frame update
    private UpgradesShopModel _dataModel { get; set; }
    private DataView _dataView { get; set; }

    public UpgradesShopController(UpgradesShopModel upgradesShopModel)
    {
        _dataModel = upgradesShopModel;
        //TODO Add Model View
        _dataModel.OnMoneyChanged += _dataView.UpdateMoney;
    }
}
