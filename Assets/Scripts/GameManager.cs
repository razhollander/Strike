﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemiesParent;
    [SerializeField] private List<SuckableObject> sceneObjects;
    [SerializeField] List<float> probabilities;
    [SerializeField] public Player player;
    [SerializeField] private float minDis, maxDis;
    [SerializeField] private float waitForSummonSceonds = 3;
    [SerializeField] private float forwardExtra = 1;
    [SerializeField] Text scoreText;
    [SerializeField] float speed;
    [SerializeField] bool isSpawn;
    [SerializeField] UpgradesShopView upgradesShopView;
    public UpgradesManager UpgradesManager;
    public DataManager DataManager;
    public static GameManager instance;

    public event Action OnGameLoad;
    public event Action OnGamePlayStart;
    public event Action OnGamePlayEnd;

    private int score;
    
    
    void Awake()
    { 
        instance = this;
        UpgradesManager = new UpgradesManager(upgradesShopView);
        DataManager = new DataManager();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        OnGamePlayStart += ()=> StartCoroutine(SummonEnemies());
        OnGamePlayEnd += () => StopCoroutine(SummonEnemies());
    }
    public void EndGame()
    {
        OnGamePlayEnd();
    }
    public void PlayGame()
    {
        OnGamePlayStart();
    }
    void Start()
    {
        OnGameLoad.Invoke();
    }
    IEnumerator SummonEnemies()
    {
        while (isSpawn)
        {
            int index = RandomFromDistribution.RandomChoiceFollowingDistribution(probabilities);
            SuckableObject spawnedObject;
            spawnedObject = sceneObjects[index].Duplicate();
            float x = UnityEngine.Random.Range(-maxDis, maxDis);

            if (x < 0 && x > -minDis)
                x = -minDis;
            else
                if (x > 0 && x < minDis)
                x = minDis;

            Vector3 vec = player.transform.right * x;
            vec += MeshHandler.GetMeshHeight(sceneObjects[index].thisRenderer) / 2 * Vector3.up;
            spawnedObject.transform.position = player.transform.forward * forwardExtra + vec + player.transform.position;
            spawnedObject.transform.SetParent(enemiesParent.transform);
            yield return new WaitForSeconds(waitForSummonSceonds);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void AddScore(int addedScore)
    {
        StartCoroutine(AddScoreCoroutine(addedScore));
       
    }
    private void UpdateScore(int addedScore)
    {
        score += addedScore;
        scoreText.text = "Score: " + score;
    }
    private IEnumerator AddScoreCoroutine(int score)
    {
        // float timePerAdd = scoreAnimationTime / (float)score;
        int scoreToAddThisFrame;
        int scoreLeftToAdd = score;
        while (scoreLeftToAdd != 0)
        { 
            scoreToAddThisFrame = Mathf.CeilToInt(Time.deltaTime * score * speed);
            if (scoreToAddThisFrame < scoreLeftToAdd)
            {
                UpdateScore(scoreToAddThisFrame);
                scoreLeftToAdd -= scoreToAddThisFrame;
            }
            else
            {
                UpdateScore(scoreLeftToAdd);
                scoreLeftToAdd -= scoreLeftToAdd;
            }
            yield return null;
        }
    }
}
