using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataView : MonoBehaviour
{
    TextMeshProUGUI moneyText;

    public void UpdateMoney(int newMoney)
    {
        moneyText.text = newMoney.ToString();
    }
}
