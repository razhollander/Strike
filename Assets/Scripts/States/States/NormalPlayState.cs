using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlayState : State
{
    GameScoreController _gameScoreController;
    public override void Enter()
    {
        base.Enter();
        _gameScoreController = new GameScoreController();
    }
    public override void Leave()
    {
        base.Leave();
        _gameScoreController.Dispose();
    }
    public void AddScore(int addedScore)
    {
        _gameScoreController.AddScore(addedScore);
    }

}
