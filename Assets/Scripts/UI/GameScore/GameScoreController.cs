using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreController
{
    const string GAME_SCORE_PREFAB_NAME = "GameScore";
    const string PLAY_CANVAS_TAG = "PlayCanvas";

    GameScoreView _gameScoreView;
    private int _score;
    public event Action<int> OnAddScore;
    public GameScoreController()
    {
        _gameScoreView = GameManager.Instance.AssetLoadHandler.CreateAsset<GameScoreView>(GAME_SCORE_PREFAB_NAME);
        _gameScoreView.transform.SetParent(GameObject.FindGameObjectWithTag(PLAY_CANVAS_TAG).transform,false);
        OnAddScore += _gameScoreView.AddScore;
    }
    public void AddScore(int addedScore)
    {
        _score += addedScore;
        OnAddScore?.Invoke(_score);
    }
}
