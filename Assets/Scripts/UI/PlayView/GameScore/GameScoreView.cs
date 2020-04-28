using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScoreView : Countable
{

    private void Awake()
    {
        BaseText = "Score:";
    }
    //public void AddScore(int addedScore)
    //{
    //    StartCoroutine(AddScoreCoroutine(addedScore));
    //}
    //private void UpdateScoreText(int addedScore)
    //{
    //    _totalScore += addedScore;
    //    _scoreText.text = SCORE_TEXT + _totalScore;
    //}
    //private IEnumerator AddScoreCoroutine(int score)
    //{
    //    int scoreToAddThisFrame;
    //    int scoreLeftToAdd = score;
    //    while (scoreLeftToAdd != 0)
    //    {
    //        scoreToAddThisFrame = Mathf.CeilToInt(Time.deltaTime * score * _scoreAnimtaionSpeed);
    //        if (scoreToAddThisFrame < scoreLeftToAdd)
    //        {
    //            UpdateScoreText(scoreToAddThisFrame);
    //            scoreLeftToAdd -= scoreToAddThisFrame;
    //        }
    //        else
    //        {
    //            UpdateScoreText(scoreLeftToAdd);
    //            scoreLeftToAdd -= scoreLeftToAdd;
    //        }
    //        yield return null;
    //    }
    //}
}
