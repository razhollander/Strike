using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusUpgradesAppearance : MonoBehaviour
{
    private float startScale = 1;
    private void Awake()
    {
        startScale = transform.localScale.x;
    }
    void Start()
    {
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += InitAppearance;
    }

    void InitAppearance()
    {
        float radiusUpgradeLevel = GameManager.Instance.UpgradesManager.GetUpgrade<RadiusUpgrader>().GetUpgradeValue();
        Vector3 newScale = Vector3.one * radiusUpgradeLevel * startScale;
        transform.localScale = newScale;
    }

}
