using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryObjectUI : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public Text text;
    public Button button;
    public void Add()
    {
        inventoryObject.count++;
        text.transform.DOPunchScale(text.transform.up,0.2f);
        UpdateText();

    }
    public void UpdateText()
    {
        text.text = "x" + inventoryObject.count;
    }
    public void Use()
    {
        inventoryObject.count--;
        UpdateText();
    }
    public void ButtonClicked()
    {
        JoystickShoot.instance.SelectObject(this);
    }

}
