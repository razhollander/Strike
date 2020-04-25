using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class BaseVehicle : OverridableMonoBehaviour
{
    public event Action<Vector2> OnJoyStickPressed;
}
