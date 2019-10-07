using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemiesParent;
    public static GameManager instance;
    public List<GameObject> sceneObjects;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        sceneObjects = new List<GameObject>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
