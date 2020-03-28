using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoadHandler
{
    PrefabsCollectionObject _prefabsCollectionObject;

    public AssetLoadHandler(PrefabsCollectionObject prefabsCollectionObject)
    {
        _prefabsCollectionObject = prefabsCollectionObject;
    }
    public T CreateAsset<T>(string name ="") where T : UnityEngine.Object
    {
        if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
        {
             return ScriptableObject.FindObjectOfType<T>() as T;
        }
        return ((GameObject)GameObject.Instantiate(_prefabsCollectionObject.prefabsDictionary[name])).GetComponent<T>();
    }
    public T GetAsset<T>(string name = "") where T : UnityEngine.Object
    {
        if (typeof(ScriptableObject).IsAssignableFrom(typeof(T)))
        {
            return ScriptableObject.FindObjectOfType<T>() as T;
        }
        return ((GameObject)_prefabsCollectionObject.prefabsDictionary[name]).GetComponent<T>();
    }
    public UnityEngine.Object LoadAsset(string name) 
    {
        return GameObject.Instantiate(_prefabsCollectionObject.prefabsDictionary[name]);
    }
}
