using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthySuckableObject : SuckableObject
{
    [SerializeField] private float healthCanvasTime = 3;
    [SerializeField] private GameObject healthCanvas;

    public SimpleHealthBar healthBar;

    private Coroutine healthCanvasTimeCoroutine;

    private float HealthLimit;

    public float maxHealth;
    protected float health;
    public float Health
    {
        get { return health; }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        health = maxHealth;
        HealthLimit = maxHealth;
        healthCanvas.SetActive(false);
        SetHealth(maxHealth);
    }
    public void SetHealth(float value, bool isRelative = true, bool isUpdateHealthLimit = false, float delay = 0)
    {
        if (value < 0)
        {
            if (healthCanvasTimeCoroutine != null)
                StopCoroutine(healthCanvasTimeCoroutine);
            healthCanvasTimeCoroutine = StartCoroutine(HealthCanvasShow());
        }
        StartCoroutine(SetHealthDelayed(value, isRelative, isUpdateHealthLimit, delay));
    }
    private IEnumerator HealthCanvasShow()
    {
        healthCanvas.SetActive(true);
        yield return new WaitForSeconds(healthCanvasTime);
        healthCanvas.SetActive(false);
    }
    public void ResetHealth()
    {
        SetHealth(maxHealth, false);
    }
    private IEnumerator SetHealthDelayed(float value, bool isRelative, bool isUpdateHealthLimit, float delay)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);
        health = isRelative ? health + value : value;
        health = health >= 0 ? health : 0;
        health = Mathf.Clamp(health, 0, HealthLimit);
        if (isUpdateHealthLimit)
            HealthLimit = health;
        healthBar.UpdateBar(health, maxHealth);
        if (health <= 0)
        {

        }
    }
}
