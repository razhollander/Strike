using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Ease ease = Ease.InSine;

    [SerializeField] private Inventory inventory;
    [SerializeField] private Button templateButton;
    [SerializeField] InventoryEffectPooledObject _inventoryEffect;
    
    public static InventoryUI instance;
    
    private List<InventoryObjectUI> inventoryObjectUIList;
    private GameManager _gm;

    void Awake()
    {
        instance = this;
        inventoryObjectUIList = new List<InventoryObjectUI>();
        PopulateInventory();
    }
    private void Start()
    {
        _gm = GameManager.Instance;
    }
    void PopulateInventory()
    {
        foreach (InventoryObject inventoryObject in inventory.inventoryObjectList)
        {
            Button newButton = Instantiate(templateButton);
            InventoryObjectUI currIOUI = newButton.transform.GetComponent<InventoryObjectUI>();
            //currIOUI.image = newButton.transform.Find("Image").GetComponent<Image>();
            currIOUI.image.sprite = inventoryObject.sprite;
            currIOUI.bgImage.sprite = inventoryObject.bgSprite;
            currIOUI.inventoryObject = inventoryObject;
            currIOUI.UpdateText();
            currIOUI.button = newButton;

            inventoryObjectUIList.Add(currIOUI);         
            newButton.gameObject.SetActive(true);
            newButton.transform.SetParent(transform);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.transform.localPosition = Vector3.zero;
            newButton.transform.localRotation = Quaternion.identity;
        }
    }
    public void StartInventoryAddEffect(SuckableobjectType suckableobjectType, Vector3 startPos)
    {
        InventoryObjectUI objectUI = GetInventoryObjectUI(suckableobjectType);
        _inventoryEffect.Get<InventoryEffectPooledObject>(objectUI.transform).StartEffect(objectUI.image.sprite, (RectTransform)objectUI.transform, startPos, () => OnEffectEnd(objectUI));
    }

    private void OnEffectEnd(InventoryObjectUI objectUI)
    {
        objectUI.Add();
        if (_gm == null)
        {
            Debug.Log("No GameManager Instance!");
        }
        int score = objectUI.inventoryObject.score;
        if (score > 0 && _gm != null)
        {
            _gm.GameStateManager.GetState<NormalPlayState>().AddScore(score);
        }
    }
    private InventoryObjectUI GetInventoryObjectUI(SuckableobjectType suckableobjectType)
    {
        return inventoryObjectUIList.Find(x => x.inventoryObject.suckableObjectType == suckableobjectType);
    }
}
