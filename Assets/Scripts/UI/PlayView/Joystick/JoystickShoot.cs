using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
public class JoystickShoot : Joystick
{
    public InventoryObjectUI selectedInventoryObject;
    [SerializeField] private ShooterVacuum shooterVacuum;
    [SerializeField] private Image _shotObjectHandleImage;
    private PostProcessVolume volume;
    Vignette vignette;
    public static JoystickShoot instance;
    public float slowTimeDuration = 0.5f;
    public float handlerRadiusToShoot = 1;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        volume = Camera.main.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
    }
    float try1(float x)
    {
        return slowTimeDuration = x;
    }
    private void SetSlowMotion(bool active)
    {
        if (active)
        {
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0.34f, slowTimeDuration);
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.5f, slowTimeDuration);
        }
        else
        {
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, 0, slowTimeDuration);
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, slowTimeDuration);
        }
    }
    public override void UpdateMe()
    {
        if(IsMouseHeld)
                  shooterVacuum.Aim(input);
        if (input.sqrMagnitude > Mathf.Pow(handlerRadiusToShoot, 2))
            shooterVacuum.SetArrow(true);
        else
            shooterVacuum.SetArrow(false);  
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        SetSlowMotion(true);
        shooterVacuum.StartAiming(input);
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        SetSlowMotion(false);
        if (input.magnitude >= handlerRadiusToShoot && selectedInventoryObject != null)
        {
            input.Normalize();
            selectedInventoryObject.Add(-1);
            shooterVacuum.Shoot(selectedInventoryObject.inventoryObject.objectShot.Get<ObjectShot>(true), input);
        }
        else
            shooterVacuum.StopAiming();
        base.OnPointerUp(eventData);
    }
    public void SelectObject(InventoryObjectUI inventoryObjectUI)
    {
        selectedInventoryObject = inventoryObjectUI;
        //handle.GetComponent<Image>().sprite = inventoryObjectUI.inventoryObject.sprite;

        if(!_shotObjectHandleImage.enabled)
        {
            _shotObjectHandleImage.color = Color.white;
        }

        _shotObjectHandleImage.sprite = inventoryObjectUI.inventoryObject.sprite;
        shooterVacuum.SetArrow(inventoryObjectUI.inventoryObject.arrowObject);
    }
}
