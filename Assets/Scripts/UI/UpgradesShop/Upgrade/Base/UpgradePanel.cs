using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private UpgradesPanelObject _upgradesPanelObject;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Transform _stockParent;
    [SerializeField] private Image _stockImage;

    [SerializeField] private Sprite _leftStockBGSprite;
    [SerializeField] private Sprite _middleStockBGSprite;
    [SerializeField] private Sprite _rightStockBGSprite;
    [SerializeField] private Sprite _soloStockBGSprite;

    private IUpgrader _upgrader;

    public void Populate(UpgradesPanelObject upgradesPanelObject, IUpgrader upgrader)
    {
        _upgradesPanelObject = upgradesPanelObject;
        _upgrader = upgrader;

        CreateStocksView();

        for (int i = 0; i < _upgradesPanelObject.CurrentUpgradeLevel; i++)
        {
            Upgrade(true);
        }
    }
    private void CreateStocksView()
    {
        var stocks = _upgradesPanelObject.UpgradeStocks;
        Debug.Log(stocks.Count);
        var numOfStocks = stocks.Count;
        Image stockImage;

        if (numOfStocks==0)
        {
            return;
        }

        if (numOfStocks == 1)
        {
            stockImage = Instantiate(_stockImage, _stockParent);
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

        }

    }
    public void Upgrade(bool isFree=false)
    {
        //TODO: check if cost is less than player money
        UpgradeStockBase currentUpgradeStock = _upgradesPanelObject.UpgradeStocks[_upgradesPanelObject.CurrentUpgradeLevel];
        _upgrader.Upgrade(_upgradesPanelObject.CurrentUpgradeLevel);
        
        //Update UI
        if (_upgradesPanelObject.CurrentUpgradeLevel >= _upgradesPanelObject.UpgradeStocks.Count)
        {
            DisablePanel();
        }
        else
        {
            if (!isFree)
            {
                GameManager.instance.player.PlayerObject.Money -= currentUpgradeStock.Cost;
                _upgradesPanelObject.CurrentUpgradeLevel++;
            }
            _costText.text = _upgradesPanelObject.UpgradeStocks[_upgradesPanelObject.CurrentUpgradeLevel].Cost.ToString();
        }
    }
    private void DisablePanel()
    {
        _buyButton.interactable = false;
    }

}
