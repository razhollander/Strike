using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusUpgradeAppearnce : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        int upgradeLevel = GameManager.Instance.UpgradesManager.GetUpgradeLevel(eUpgradeType.Radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
