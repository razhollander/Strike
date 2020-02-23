using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager
{
    public UpgradesShopController UpgradesShopController;
    public UpgradesShopModel UpgradesShopModel;
    public UpgradesShopView UpgradesShopView;

    public ShopManager()
    {
        UpgradesShopView = GameObject.Instantiate<UpgradesShopView>(Resources.Load<UpgradesShopView>(""));
        UpgradesShopModel = new UpgradesShopModel();
        UpgradesShopController = new UpgradesShopController(UpgradesShopModel);
    }
}
