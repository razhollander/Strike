using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    public Ease ease;

    [SerializeField] private Inventory inventory;
    [SerializeField] private Button templateButton;

    private static List<InventoryObjectUI> inventoryObjectUIList;
    public static InventoryUI instance;
    void Awake()
    {
        instance = this;
        inventoryObjectUIList = new List<InventoryObjectUI>();
        PopulateInventory();
    }

    void PopulateInventory()
    {
        foreach (InventoryObject inventoryObject in inventory.inventoryObjectList)
        {
            Button newButton = Instantiate(templateButton);
            InventoryObjectUI currIOUI = newButton.transform.GetComponent<InventoryObjectUI>();
            currIOUI.image = newButton.transform.Find("Image").GetComponent<Image>();
            currIOUI.image.sprite = inventoryObject.sprite;
            currIOUI.inventoryObject = inventoryObject;
            currIOUI.UpdateText();
            currIOUI.button = newButton;
            inventoryObjectUIList.Add(currIOUI);         
            newButton.gameObject.SetActive(true);
            newButton.transform.SetParent(transform);
            newButton.transform.localScale = Vector3.one;
        }
    }
    public void StartAddEffect(SuckableobjectType suckableobjectType, Vector3 startPos)
    {
       StartCoroutine(StartAddEffectCoroutin(suckableobjectType, startPos));
    }
    private IEnumerator StartAddEffectCoroutin(SuckableobjectType suckableobjectType, Vector3 startPos)
    {
        InventoryObjectUI objectUI = GetInventoryObjectUI(suckableobjectType);
        Image img = Instantiate(objectUI.image);
        Vector3 endPos = Vector3.one;
        
        img.transform.SetParent(objectUI.transform);
        img.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(startPos);
        img.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        float waitForSeceonds = 0.5f;
        img.transform.DOScale(1, waitForSeceonds).SetEase(Ease.OutExpo);
        img.transform.DOLocalMove(endPos, waitForSeceonds).SetEase(ease);
        yield return new WaitForSeconds(waitForSeceonds);
        Destroy(img.gameObject);
        objectUI.Add();
        if(GameManager.Instance == null)
        {
            Debug.Log("No GameManager Instance!");
        }
        int score = objectUI.inventoryObject.score;
        if (score > 0&& GameManager.Instance!=null)
            GameManager.Instance.AddScore(score);
    }

    public static InventoryObjectUI GetInventoryObjectUI(SuckableobjectType suckableobjectType)
    {
        return inventoryObjectUIList.Find(x => x.inventoryObject.suckableObjectType == suckableobjectType);
    }
}
