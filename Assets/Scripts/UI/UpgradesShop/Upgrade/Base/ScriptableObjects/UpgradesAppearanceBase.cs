using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradesAppearanceBase : MonoBehaviour
{
    const string BUY_EFFECT_NAME = "BoughtLightningEffect";
    private bool _isDidInit = false;

    [SerializeField]
    protected UpgradeAppearanceBaseObject UpgradeAppearanceObject;

    private BuyEffect buyEffect;
    protected abstract void OnUpgrade(int level);

    void Start()
    {
        buyEffect = GameManager.Instance.AssetLoadHandler.GetAsset<BuyEffect>(BUY_EFFECT_NAME);
        GameManager.Instance.UpgradesManager.GetUpgrade(UpgradeAppearanceObject.UpgradeType).OnUpgrade += OnUpgrade;
        InitAppearance();
        _isDidInit = true;
    }
    protected virtual void InitAppearance()
    {
        OnUpgrade(GameManager.Instance.UpgradesManager.GetUpgradeLevel(UpgradeAppearanceObject.UpgradeType));
    }
    protected void DoBuyEffect(Transform upgradedObject)
    {
        if (_isDidInit)
        {
            Transform effect = buyEffect.Get<BuyEffect>().transform;
            effect.position = upgradedObject.position;
            effect.GetComponent<ParticleToTarget>().Target = upgradedObject;
        }
    }
}
