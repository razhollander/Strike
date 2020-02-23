using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    const string PLAYER_MONEY = "PlayerMoney";

    public DataManager()
    {
    }

    public int PlayerMoney
    {
        get { return PlayerPrefs.GetInt(PLAYER_MONEY); }
        set { PlayerPrefs.SetInt(PLAYER_MONEY,value); }
    }

}
