using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshHandler
{
    public static float GetMeshHeight(Renderer meshRenderer)
    {
        return meshRenderer.bounds.size.y;
    }
    //public static float GetMeshHeight(Renderer rend)
    //{
        ////Debug.Log(gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y);
        ////Renderer rend = gameObject.GetComponentInChildren<Renderer>();
        //Debug.Log(rend);
        //if (rend is MeshRenderer)
        //     return gameObject.GetComponentInChildren<MeshRenderer>().bounds.size.y;
        //if(rend is SkinnedMeshRenderer)
        //    return gameObject.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;
        //Debug.Log("Null Renderer");
        //return 0;
    //}
}
