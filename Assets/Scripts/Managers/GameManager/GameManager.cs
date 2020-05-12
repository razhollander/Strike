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
    [SerializeField] public PlayerBase player;
    [SerializeField] private float minDis, maxDis;
    [SerializeField] private float waitForSummonSceonds = 3;
    [SerializeField] private float forwardExtra = 1;
    [SerializeField] bool isSpawn;
    [SerializeField] PrefabsCollectionObject _prefabsCollectionObject;

    public static GameManager Instance;

    public UpgradesManager UpgradesManager;
    public AssetLoadHandler AssetLoadHandler;
    public GamePrefHandler GamePrefHandler;
    public GameStateManager GameStateManager;
    public MoneyModel MoneyModel;

    public event Action OnGamePaused;
    public event Action OnGameResumed;


    private Coroutine summonCorutine;

    void Awake()
    {
        Instance = this;
        GamePrefHandler = new GamePrefHandler();
        AssetLoadHandler = new AssetLoadHandler(_prefabsCollectionObject);
        UpgradesManager = new UpgradesManager();
        GameStateManager = new GameStateManager();
        MoneyModel = new MoneyModel();
        player = GameObject.FindObjectOfType<PlayerBase>();

        //Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;

        var normalPlayState = GameStateManager.GetState<NormalPlayState>();
        normalPlayState.OnEnter+= ()=> summonCorutine = StartCoroutine(SummonEnemies());
        normalPlayState.OnLeave+= () => { if (summonCorutine != null) StopCoroutine(summonCorutine); };
    }
    public List<SuckableObject> GetSuckableObjects()
    {
        return enemiesParent.transform.GetComponentsInChildren<SuckableObject>().ToList();
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
        GameStateManager.SwitchGameState<MainMenuState>();
    }
    public void PlayGame()
    {
        GameStateManager.SwitchGameState<NormalPlayState>();
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

            Vector3 vec = player.transform.right.SetYZero() * x;
            //vec += (MeshHandler.GetMeshHeight(sceneObjects[index].thisRenderer) / 2+ 0.1f )* Vector3.up;
            //spawnedObject.transform.position = player.transform.forward * forwardExtra + vec + player.transform.position;
            spawnedObject.SetSpawnedPosition(player.transform.forward * forwardExtra + vec + player.transform.position);
            spawnedObject.transform.SetParent(enemiesParent.transform);
            yield return new WaitForSeconds(waitForSummonSceonds);
        }
    }

}
