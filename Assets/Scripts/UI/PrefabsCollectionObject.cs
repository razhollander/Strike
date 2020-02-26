using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsCollectionObject", menuName = "GameManager/PrefabsCollectionObject")]
public class PrefabsCollectionObject : ScriptableObject
{
    public PrefabsDictionary prefabsDictionary;
}
[System.Serializable] public class PrefabsDictionary : SerializableDictionary<string, Object> { }

