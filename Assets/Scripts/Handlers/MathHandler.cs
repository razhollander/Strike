using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHandler
{
    public static Vector2 Vector3ToVector2(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }
}
