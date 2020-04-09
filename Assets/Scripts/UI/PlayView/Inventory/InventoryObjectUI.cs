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
    private void Start()
    {
        ResetCollected();
        var normalPlayState = GameManager.Instance.GameStateManager.GetState<NormalPlayState>();
        normalPlayState.OnEnter += ResetCollected;
        normalPlayState.OnEnter += ResetCollected;

    }

    public void Add(int counter=1)
    {
        inventoryObject.collected+= counter;
        punchTweener.Restart();
        punchTweener.Kill();
        punchTweener = text.transform.DOPunchScale(text.transform.up,0.2f);
        UpdateText();
    }

    public void UpdateText()
    {
        text.text = "x" + inventoryObject.collected;
    }
    public void Use()
    {
        inventoryObject.collected--;
        UpdateText();
    }
    public void ButtonClicked()
    {
        JoystickShoot.instance.SelectObject(this);
    }
    private void ResetCollected()
    {
        inventoryObject.collected = 0;
        UpdateText();
    }
}
