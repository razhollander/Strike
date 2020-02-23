﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradePanel : MonoBehaviour
{
    const string DEFAULT_COST_TEXT = "-";

    [SerializeField] private UpgradesPanelObject _upgradesPanelObject;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _TitleText;

    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _stockParent;
    [SerializeField] private Image _stockImage;
    [SerializeField] private Color _boughtColor;

    [SerializeField] private Sprite _leftStockBGSprite;
    [SerializeField] private Sprite _middleStockBGSprite;
    [SerializeField] private Sprite _rightStockBGSprite;
    [SerializeField] private Sprite _soloStockBGSprite;

    List<Image> stocksImages;
    private IUpgrader _upgrader;

    public void SetPanel(UpgradesPanelObject upgradesPanelObject, IUpgrader upgrader)
    {
        stocksImages = new List<Image>();
        _upgradesPanelObject = upgradesPanelObject;
        _upgrader = upgrader;
        _TitleText.text = upgradesPanelObject.Name;

        CreateStocksView();

        int currentUpgradeLevel = GameManager.instance.shopManager.UpgradesShopModel.GetUpgradeLevel(upgradesPanelObject.EUpgradeType);

        for (int i = 0; i < currentUpgradeLevel; i++)
        {
            if(i<_upgradesPanelObject.UpgradeStocks.Count)
                OnEnableUpgrade(i);
        }
    }

    private void CreateStocksView()
    {
        var stocks = _upgradesPanelObject.UpgradeStocks;
        var numOfStocks = stocks.Count;
        Image stockImage;

        if (numOfStocks==0)
        {
            return;
        }

        if (numOfStocks == 1)
        {
            stockImage = Instantiate(_stockImage, _stockParent);
            stockImage.sprite = _soloStockBGSprite;
            stocksImages.Add(stockImage);
            return;
        }

        for (int i = 0; i < numOfStocks; i++)
        {
            stockImage = Instantiate(_stockImage, _stockParent);

            if (i==0)
            {
                stockImage.sprite = _leftStockBGSprite;
            }
            else
            {
                if(i==numOfStocks-1)
                {
                    stockImage.sprite = _rightStockBGSprite;
                }
                else
                {
                    stockImage.sprite = _middleStockBGSprite;
                }
            }

            stocksImages.Add(stockImage);
        }

    }
    private void OnEnableUpgrade(int currUpgradeLevel = 0)
    {
        UpgradeStockBase currentUpgradeStock = _upgradesPanelObject.UpgradeStocks[currUpgradeLevel];
        string costText = DEFAULT_COST_TEXT;
            
        _upgrader.Upgrade(currUpgradeLevel, currentUpgradeStock);
        stocksImages[currUpgradeLevel].color = _boughtColor;
        currUpgradeLevel++;

        if (currUpgradeLevel < _upgradesPanelObject.UpgradeStocks.Count)
        {
            costText = _upgradesPanelObject.UpgradeStocks[currUpgradeLevel].Cost.ToString();
        }
        else
        {
            DisablePanel();
        }

        _costText.text = costText;
    }
    public void Upgrade()
    {
        //TODO: check if cost is less than player money
        int currUpgradeLevel = GameManager.instance.shopManager.UpgradesShopModel.GetUpgradeLevel(_upgradesPanelObject.EUpgradeType);

        UpgradeStockBase currentUpgradeStock = _upgradesPanelObject.UpgradeStocks[currUpgradeLevel];
        string costText = DEFAULT_COST_TEXT;

        //Update UI
        if (currUpgradeLevel >= _upgradesPanelObject.UpgradeStocks.Count)
        {
            return;
        }
        else
        {
            stocksImages[currUpgradeLevel].color = _boughtColor;
            _upgrader.Upgrade(currUpgradeLevel, currentUpgradeStock);
            GameManager.instance.player.PlayerObject.Money -= currentUpgradeStock.Cost;
            GameManager.instance.shopManager.UpgradesShopModel.SetUpgradeLevel(_upgradesPanelObject.EUpgradeType, currUpgradeLevel++);

            if (currUpgradeLevel < _upgradesPanelObject.UpgradeStocks.Count)
            {
                costText = _upgradesPanelObject.UpgradeStocks[currUpgradeLevel].Cost.ToString();
            }
            else
            {
                DisablePanel();
            }

            _costText.text = costText;
        }
    }
    private void DisablePanel()
    {
        _buyButton.interactable = false;
    }

}
