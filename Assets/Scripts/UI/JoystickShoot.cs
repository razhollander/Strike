using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickShoot : Joystick
{
    public InventoryObjectUI selectedInventoryObject;
    [SerializeField] private ShooterVacuum shooterObject;

    public static JoystickShoot instance;
    bool isMouseHeld = false;
    private void Awake()
    {
        instance = this;
    }
    private void LateUpdate()
    {
        if(isMouseHeld)
            shooterObject.Aim(input);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        Time.timeScale = 0.5f;
        isMouseHeld = true;
        shooterObject.StartAiming(input);
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1;
        isMouseHeld = false;
        if (input.magnitude >= 0.95 && selectedInventoryObject != null)
        {
            input.Normalize();
            selectedInventoryObject.Add(-1);
            shooterObject.Shoot(Instantiate(selectedInventoryObject.inventoryObject.objectShot), input);
        }
        else
            shooterObject.StopAiming();
        base.OnPointerUp(eventData);
    }
    public void SelectObject(InventoryObjectUI inventoryObject)
    {
        selectedInventoryObject = inventoryObject;
        handle.GetComponent<Image>().sprite = inventoryObject.inventoryObject.sprite;
    }
}
