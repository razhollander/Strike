using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusUpgradesAppearance : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.GameStateManager.NormalPlay.OnEnter += InitAppearance;
    }

    void InitAppearance()
    {
        float radiusUpgradeLevel = GameManager.Instance.UpgradesManager.GetUpgrade<RadiusUpgrader>().GetUpgradeValue();
        Vector3 newScale = Vector3.one * radiusUpgradeLevel;
        transform.localScale = newScale;
    }

}
