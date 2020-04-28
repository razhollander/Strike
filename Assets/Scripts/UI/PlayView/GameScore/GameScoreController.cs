using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreController
{
    const string GAME_SCORE_PREFAB_NAME = "GameScore";

    GameScoreView _gameScoreView;
    int _score;
    public int Score {
        get { return _score; }
        set
        {
            _score = value;
            OnAddScore?.Invoke(_score);
        }
    }
    
    private event Action<int> OnAddScore;
    public GameScoreController()
    {
        _gameScoreView = GameManager.Instance.AssetLoadHandler.CreateAsset<GameScoreView>(GAME_SCORE_PREFAB_NAME);
        _gameScoreView.transform.SetParent(GameObject.FindGameObjectWithTag(PlayView.PLAY_CANVAS_TAG).transform,false);
        OnAddScore += _gameScoreView.SetNumber;
    }
    public void Dispose()
    {
        OnAddScore -= _gameScoreView.SetNumber;
        GameObject.Destroy(_gameScoreView.gameObject);
    }
}
