using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshHandler
{
    public static float GetMeshHeight(MeshRenderer meshRenderer)
    {
        return meshRenderer.bounds.size.y;
    }
    public static float GetMeshHeight(GameObject gameObject)
    {
        //Debug.Log(gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y);
        return gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }
}
