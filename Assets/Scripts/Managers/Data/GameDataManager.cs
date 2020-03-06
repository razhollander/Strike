using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameDataManager
{
    const string PLAYER_MONEY = "PlayerMoney";
    public event Action<int> OnPlayerMoneyChanged;
    public int Money
    {
        get
        {
            if(!GameManager.Instance.GamePrefHandler.IsKeyExist(PLAYER_MONEY))
            {
                GameManager.Instance.GamePrefHandler.SavePref<int>(0, PLAYER_MONEY);
            }
            return GameManager.Instance.GamePrefHandler.LoadPref<int>(PLAYER_MONEY);
        }
        set
        {
            GameManager.Instance.GamePrefHandler.SavePref<int>(value ,PLAYER_MONEY);
            OnPlayerMoneyChanged(value);
        }
    }
}
