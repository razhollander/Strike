using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemiesParent;
    public static GameManager instance;
    public List<GameObject> sceneObjects;
    public GameObject player;
    public float minDis, maxDis;
    public float waitForSummonSceonds = 3;
    public float forwardExtra = 1;
    
    // Start is called before the first frame update
    void Start()
    { 
        instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(SummonEnemies());

        
    }
    IEnumerator SummonEnemies()
    {
        while (true)
        {
            GameObject go = Instantiate(sceneObjects[0]);
            float x = Random.Range(-maxDis, maxDis);

            if (x < 0 && x > -minDis)
                x = -minDis;
            else
                if (x > 0 && x < minDis)
                x = minDis;

            Vector3 vec = player.transform.right * x;
            vec += MeshHandler.GetMeshHeight(go)/2 * Vector3.up;
            go.transform.position =player.transform.forward* forwardExtra + vec + player.transform.position;
            go.transform.SetParent(enemiesParent.transform);
            yield return new WaitForSeconds(waitForSummonSceonds);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
