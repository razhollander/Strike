using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
public class Enemy : SuckableObject
{
    [SerializeField]
    private bool canBeSucked = true;
    public float health;
    public SimpleHealthBar healthBar;
    private float maxHealth=100;
    private float HealthLimit = 100;
    //public int Number = 0;
    public bool CanBeSucked()
    {
        return canBeSucked;
    }

    void Awake()
    {
        maxHealth = health;
        HealthLimit = maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            SetHealth(0, false, true);
        }
    }
    public void SetHealth(float value, bool isRelative=true, bool isUpdateHealthLimit=false)
    {
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
}
