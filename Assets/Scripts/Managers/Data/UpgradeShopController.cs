using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShopController
{
    // Start is called before the first frame update
    private UpgradesShopModel _dataModel { get; set; }
    private DataView _dataView { get; set; }

    public UpgradesShopController(UpgradesShopModel dataModel)
    {
        _dataModel = dataModel;
        _dataModel.OnMoneyChanged += _dataView.UpdateMoney;
    }
}
