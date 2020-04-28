using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayState : State
{
    GameScoreController _gameScoreController;
    GameMoneyController _gameMoneyController;
    public override void Enter()
    {
        base.Enter();
        _gameScoreController = new GameScoreController();
        _gameMoneyController = new GameMoneyController();
    }
    public override void Leave()
    {
        base.Leave();
        _gameScoreController.Dispose();
        _gameMoneyController.Dispose();
    }
    public void AddScore(int addedScore)
    {
        _gameScoreController.Score += addedScore;
    }
    public void AddGameMoney(int addedGameMoney, Vector3 suckedPosition)
    {
        _gameMoneyController.AddGameMoney(addedGameMoney, suckedPosition);
    }

}
