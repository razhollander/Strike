using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InventoryUI : MonoBehaviour
{
    public Ease ease;

    [SerializeField] private Inventory inventory;
    [SerializeField] private Canvas mainCanvas;
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
            newButton.transform.SetParent(transform);
            
            newButton.transform.Find("Image").GetComponent<Image>().sprite = inventoryObject.sprite;
            newButton.gameObject.SetActive(true);
            InventoryObjectUI currIOUI = newButton.transform.GetComponent<InventoryObjectUI>();
            currIOUI.image = newButton.transform.Find("Image").GetComponent<Image>();
            currIOUI.inventoryObject = inventoryObject;
            currIOUI.UpdateText();
            currIOUI.button = newButton;
            inventoryObjectUIList.Add(currIOUI);
            
        }
    }
    public void StartAddEffect(SuckableobjectType suckableobjectType, Vector3 startPos)
    {
       StartCoroutine(StartAddEffectCoursotin(suckableobjectType, startPos));
    }
    private IEnumerator StartAddEffectCoursotin(SuckableobjectType suckableobjectType, Vector3 startPos)
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
        if(GameManager.instance == null)
        {
            Debug.Log("No GameManager Instance!");
        }
        int score = objectUI.inventoryObject.score;
        if (score > 0&& GameManager.instance!=null)
            GameManager.instance.AddScore(score);
    }
    private void EndAddEffect()
    {

    }
    public static InventoryObjectUI GetInventoryObjectUI(SuckableobjectType suckableobjectType)
    {
        return inventoryObjectUIList.Find(x => x.inventoryObject.suckableObjectType == suckableobjectType);
    }
}
