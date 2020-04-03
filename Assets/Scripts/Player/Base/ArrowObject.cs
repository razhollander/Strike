using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrowObject", menuName = "ArrowObject")]

public class ArrowObject : ScriptableObject
{
    public float width;
    public Sprite sprite;
    public bool isHasChildSprite;
    public Sprite childSprite;

}
