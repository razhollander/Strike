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
    [SerializeField] protected FollowPlayer thisFollowPlayer;

    public float maxHealth;
    [SerializeField] protected float spawnTimeDelay = 0.2f;
    [SerializeField] private float healthCanvasTime=3;
    [SerializeField] private GameObject healthCanvas;
    private Coroutine healthCanvasTimeCoroutine;
    protected float health;
    public SimpleHealthBar healthBar;
    private float HealthLimit;
    private float timeToDie = 3;
    protected event Action dieEvent;
    protected event Action startDyingEvent;

    //public int Number = 0;
    protected override void Awake()
    {
        base.Awake();
        startDyingEvent += StartDying;
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
        thisFollowPlayer.enabled = true;
        health = maxHealth;
        HealthLimit = maxHealth;
        healthCanvas.SetActive(false);
        SetHealth(maxHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            startDyingEvent();
        }
    }
    private void StartDying()
    {
        StartCoroutine(StartDyingCoroutine());
    }
    private IEnumerator StartDyingCoroutine()
    {
        SetHealth(0, false, true);
        thisFollowPlayer.enabled = false;
        yield return new WaitForSeconds(timeToDie);
        if(!IsBeingSucked)
             gameObject.SetActive(false);
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
    //public void AddForce(Vector3 force)
    //{
    //    thisRigidBody.AddForce(force, ForceMode.Force);
    //}
    public void AddForce(Vector3 force,float delay)
    {
        StartCoroutine(AddForceDelay(force, delay));
    }
    private IEnumerator AddForceDelay(Vector3 force,float delay)
    {
        yield return new WaitForSeconds(delay);
        thisRigidBody.AddForce(force, ForceMode.Force);
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
        if (health<=0)
        {

        }
    }
    //protected void SetCenterOfMass()
    //{
    //    GetComponentInChildren<Rigidbody>().centerOfMass = Vector3.down;
    //}
    //public override SuckableObject Duplicate()
    //{
    //    return this.Get<Enemy>();
    //}
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
    }
    protected override void MakeActive(bool isActive)
    {
        thisFollowPlayer.enabled = isActive;
        base.MakeActive(isActive);
    }
}
