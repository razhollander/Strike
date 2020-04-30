using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMoneyController
{
    const string GAME_MONEY_PREFAB_NAME = "GameMoney";
    const string NORMAL_PLAY_TOP_RIGHT_PANEL = "NormalPlayTopRightPanel";
    GameMoneyView _gameMoneyView;

    int _gameMoney;
    public int GameMoney
    {
        get
        {
            return _gameMoney;
        }
        set
        {
            _gameMoney = value;
            OnAddGameMoney?.Invoke(_gameMoney);
        }
    }

    private event Action<int> OnAddGameMoney;

    // Start is called before the first frame update
    public GameMoneyController()
    {
        _gameMoneyView = GameManager.Instance.AssetLoadHandler.CreateAsset<GameMoneyView>(GAME_MONEY_PREFAB_NAME);
        _gameMoneyView.transform.SetParent(GameObject.FindGameObjectWithTag(NORMAL_PLAY_TOP_RIGHT_PANEL).transform, false);
        //OnAddGameMoney += _gameMoneyView.SetNumber;
    }
    public void AddGameMoney(int addedGameMoney, Vector3 suckedPosition)
    {
        _gameMoney += addedGameMoney;
        _gameMoneyView.StartEffectCorutine(_gameMoney, suckedPosition);
    }
    public void Dispose()
    {
        //OnAddGameMoney -= _gameMoneyView.SetNumber;
        GameObject.Destroy(_gameMoneyView.gameObject);
    }
}
