﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIAddEffectBase : PooledMonobehaviour
{
    [Header("Inventory Effect Pooled Object")]
    [SerializeField]
    protected Image image;
    [SerializeField]
    private Ease _ease = Ease.InQuad;

    [SerializeField]
    float _startScale = 0.1f;
    [SerializeField]
    int _rotationAmount = 1440;
    [SerializeField]
    float _animationTime = 1;

    RectTransform _rectTransform;
    Vector2 _resolutionMultiplier;
    CanvasScaler _masterCanvasScaler;
    private void Awake()
    {
        _rectTransform = (RectTransform)transform;
        image.enabled = false;
        _masterCanvasScaler = GameObject.FindGameObjectWithTag("MasterCanvas").GetComponent<CanvasScaler>();
    }

    private void OnEnable()
    {
        Vector2 referenceResolution = _masterCanvasScaler.referenceResolution;
        Vector2 screenWidth = new Vector2(Screen.width, Screen.height);
        _resolutionMultiplier = referenceResolution / screenWidth;
    }
    public void StartEffect(Sprite sprite, RectTransform parentTransfrom, Vector3 startWorldPos, Action OnArrive = null)
    {
        image.sprite = sprite;
        StartCoroutine(StartEffectCoroutine(parentTransfrom, startWorldPos, OnArrive));
    }

    private IEnumerator StartEffectCoroutine(RectTransform parentTransfrom, Vector3 startWorldPos, Action OnArrive = null)
    {
        yield return new WaitForEndOfFrame();

        Vector3 spawnPointScreenSpace = CameraManager.instance.MainCamera.WorldToScreenPoint(startWorldPos);
        Vector3 thisPointScreenSpace = CameraManager.instance.MainCamera.WorldToScreenPoint(parentTransfrom.position);
        _rectTransform.localPosition = (spawnPointScreenSpace - thisPointScreenSpace) * _resolutionMultiplier;

        image.enabled = true;

        _rectTransform.localRotation = Quaternion.identity;
        _rectTransform.localScale = _startScale * Vector3.one;

        transform.DOLocalMove(Vector3.zero, _animationTime).SetEase(_ease);
        transform.DOScale(1, _animationTime).SetEase(Ease.OutExpo);
        transform.DOLocalRotate(Vector3.up * _rotationAmount, _animationTime, RotateMode.LocalAxisAdd).SetEase(_ease);

        yield return new WaitForSeconds(_animationTime);

        OnArrive?.Invoke();
        this.OnArrive();
    }
    protected virtual void OnArrive()
    {
        gameObject.SetActive(false);
    }
}
