using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryObjectUI : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public Image image;
    public Button button;
    [SerializeField] private Text text;
    private Tween punchTweener;
    public void Add(int counter=1)
    {
        inventoryObject.count+= counter;
        punchTweener.Restart();
        punchTweener.Kill();
        punchTweener = text.transform.DOPunchScale(text.transform.up,0.2f);
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
