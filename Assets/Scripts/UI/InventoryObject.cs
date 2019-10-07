using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryObject", menuName = "Inventory/InventoryObject")]
public class InventoryObject : ScriptableObject
{
    public SuckableobjectType suckableObjectType;
    public GameObject objectShot;
    public int count = 0;
    public Sprite sprite;



}
