using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    const float TIMESCALE_CHNGE_DAURATION = 0.4f;

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
    [SerializeField] PrefabsCollectionObject _prefabsCollectionObject;

    public static GameManager Instance;

    public UpgradesManager UpgradesManager;
    public AssetLoadHandler AssetLoadHandler;
    public GamePrefHandler GamePrefHandler;
    public GameDataManager GameDataManager;
    public GameStateManager GameStateManager;

    public event Action OnGamePaused;
    public event Action OnGameResumed;


    private int score;
    private Coroutine summonCorutine;

    void Awake()
    {
        Instance = this;
        GamePrefHandler = new GamePrefHandler();
        GameDataManager = new GameDataManager();
        AssetLoadHandler = new AssetLoadHandler(_prefabsCollectionObject);
        UpgradesManager = new UpgradesManager();
        GameStateManager = new GameStateManager();

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        GameStateManager.NormalPlay.OnEnter += ()=> summonCorutine = StartCoroutine(SummonEnemies());
        GameStateManager.NormalPlay.OnLeave += () => StopCoroutine(summonCorutine);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }
    public void ResumeGame()
    {
        Time.timeScale = 0.3f;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1f, TIMESCALE_CHNGE_DAURATION);
        OnGameResumed?.Invoke();
    }
    public void EndGame()
    {
        Time.timeScale = 1f;
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<ISceneObject>();
        foreach (ISceneObject s in ss)
        {
            s.DoQuitAnimation();
        }
        GameStateManager.SwitchGameState(GameStateManager.MainMenu);
    }
    public void PlayGame()
    {
        GameStateManager.SwitchGameState(GameManager.Instance.GameStateManager.NormalPlay);
        Debug.Log("Start game");
    }
    void Start()
    {
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
    public T GetPrefab<T>(string name) where T : UnityEngine.Object
    {
        return (T)_prefabsCollectionObject.prefabsDictionary[name.Equals(name)];
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
