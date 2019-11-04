using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Threading.Tasks;
public class Enemy : SuckableObject
{
    [SerializeField]
    private bool canBeSucked = true;
    [SerializeField] FollowPlayer followPlayer;
    public float maxHealth;
    protected float health;
    public SimpleHealthBar healthBar;
    private float HealthLimit;
    private float timeToDie = 3;
    protected event Action OnStartDying;
    //public int Number = 0;
    public virtual void Start()
    {
        OnStartDying += StartDying;
    }
    public bool CanBeSucked()
    {
        return canBeSucked;
    }
    public float Health
    {
        get { return health; }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        followPlayer.enabled = true;
        health = maxHealth;
        HealthLimit = maxHealth;
        SetHealth(maxHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnStartDying.Invoke();
        }
    }
    private void StartDying()
    {
        StartCoroutine(StartDyingCoroutine());
    }
    private IEnumerator StartDyingCoroutine()
    {
        SetHealth(0, false, true);
        followPlayer.enabled = false;
        yield return 3;
        yield return new WaitForSeconds(timeToDie);
        gameObject.SetActive(false);
    }
    public void SetHealth(float value, bool isRelative = true, bool isUpdateHealthLimit = false, float delay = 0)
    {
        StartCoroutine(SetHealthDelayed(value, isRelative, isUpdateHealthLimit, delay));
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
    }
    public void ResetHealth()
    {
        SetHealth(maxHealth, false);
    }
    protected void SetCenterOfMass()
    {
        GetComponentInChildren<Rigidbody>().centerOfMass = Vector3.down;
    }
    public override SuckableObject Duplicate()
    {
        return this.Get<Enemy>();
    }
}
