using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraHandler:MonoBehaviour
{
    Tween shakeTween;
    public static CameraHandler instance;
    [SerializeField] Camera mainCamera;
    private void Awake()
    {
        instance = this;
    }
    public void ShakeCamera(float darution = 0.5f, float strength=1)
    {
        shakeTween.Kill();
        shakeTween = mainCamera.transform.DOShakeRotation(darution, strength);
    }
}
