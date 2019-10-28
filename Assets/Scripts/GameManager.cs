using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject enemiesParent;
    public static GameManager instance;
    public List<SuckableObject> sceneObjects;
    public GameObject player;
    public float minDis, maxDis;
    public float waitForSummonSceonds = 3;
    public float forwardExtra = 1;
    [SerializeField] List<float> probabilities;
    // Start is called before the first frame update
    void Start()
    { 
        instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(SummonEnemies());

        //probabilities = new List<float> { 4, 10, 4 };

    }
    IEnumerator SummonEnemies()
    {
        while (true)
        {
            int index = RandomFromDistribution.RandomChoiceFollowingDistribution(probabilities);
            GameObject go;
            go = sceneObjects[index].Duplicate().gameObject;
            float x = Random.Range(-maxDis, maxDis);

            if (x < 0 && x > -minDis)
                x = -minDis;
            else
                if (x > 0 && x < minDis)
                x = minDis;

            Vector3 vec = player.transform.right * x;
            vec += MeshHandler.GetMeshHeight(go) / 2 * Vector3.up;
            go.transform.position = player.transform.forward * forwardExtra + vec + player.transform.position;
            go.transform.SetParent(enemiesParent.transform);
            yield return new WaitForSeconds(waitForSummonSceonds);
        }
    }
    //// Update is called once per frame
    //void Update()
    //{

    //}
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
