using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickShoot : Joystick
{
    public InventoryObjectUI selectedInventoryObject;
    [SerializeField] private Shooter shooterObject;

    public static JoystickShoot instance;

    private void Awake()
    {
        instance = this;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        shooterObject.StartAiming();
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (input.magnitude == 1)
            shooterObject.Shoot(selectedInventoryObject.inventoryObject.objectShot,input);
        else
            shooterObject.StopAiming();
        base.OnPointerUp(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = Vector2.one;
        shooterObject.Aim(direction);
        base.OnDrag(eventData);
    }
    public void SelectObject(InventoryObjectUI inventoryObject)
    {
        selectedInventoryObject = inventoryObject;
        handle.GetComponent<Image>().sprite = inventoryObject.inventoryObject.sprite;
    }


}
