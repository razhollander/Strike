using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    [SerializeField] private RectTransform background = null;
    [SerializeField] private RectTransform handle = null;
    [SerializeField] private Player player = null;
    [SerializeField] private VehicleBehaviour.WheelVehicle wheelV;
    private RectTransform baseRect = null;
    private Canvas canvas;
    private Camera cam;
    private Vector2 handleStartPos;
    float maxX, minY;
    void Start()
    {
        handleStartPos = handle.anchoredPosition;
        maxX = background.sizeDelta.x / 3;
        minY = handleStartPos.y - background.sizeDelta.y / 2;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        Vector2 backGroundPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);       
        SetHandle(eventData.position - backGroundPosition);
        SetPlayerParameters();
    }

    private void SetPlayerParameters()
    {
        wheelV.steerValue = (handle.anchoredPosition.x - handleStartPos.x) / maxX;
       
    }

    private void SetHandle(Vector2 deltaFromCenter)
    {
        float x = (Mathf.Clamp(deltaFromCenter.x, -maxX, maxX));
        float y = handleStartPos.y;

        if (Mathf.Abs(x) == maxX)
        {
            y = Mathf.Clamp(deltaFromCenter.y, minY,handleStartPos.y);
        }

        handle.anchoredPosition = new Vector2(x, y);
    }
    
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        handle.anchoredPosition = handleStartPos;
        SetPlayerParameters();
    }

    
}

