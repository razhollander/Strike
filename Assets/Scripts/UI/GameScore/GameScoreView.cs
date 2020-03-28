using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScoreView : MonoBehaviour
{
    [SerializeField] float _scoreAnimtaionSpeed;
    [SerializeField] Text _scoreText;


    public void AddScore(int addedScore)
    {
        StartCoroutine(AddScoreCoroutine(addedScore));
    }
    private void UpdateScoreText(int addedScore)
    {
        _scoreText.text = "Score: " + addedScore;
    }
    private IEnumerator AddScoreCoroutine(int score)
    {
        int scoreToAddThisFrame;
        int scoreLeftToAdd = score;
        while (scoreLeftToAdd != 0)
        {
            scoreToAddThisFrame = Mathf.CeilToInt(Time.deltaTime * score * _scoreAnimtaionSpeed);
            if (scoreToAddThisFrame < scoreLeftToAdd)
            {
                UpdateScoreText(scoreToAddThisFrame);
                scoreLeftToAdd -= scoreToAddThisFrame;
            }
            else
            {
                UpdateScoreText(scoreLeftToAdd);
                scoreLeftToAdd -= scoreLeftToAdd;
            }
            yield return null;
        }
    }
}
