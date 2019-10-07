using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : SuckableObject
{
    [SerializeField]
    private bool canBeSucked = true;
    public float health;
    public SimpleHealthBar healthBar;
    private float maxHealth=100;
    public bool CanBeSucked()
    {
        return canBeSucked;
    }
    void Start()
    {
        maxHealth = health;
        //health = maxHealth; 
    }
    
    public void SetHealth(float value, bool isRelative=true)
    {
        health = isRelative ? health + value : value;
        health = health >= 0 ? health : 0;
        healthBar.UpdateBar(health, maxHealth);
    }
    public void ResetHealth()
    {
        SetHealth(maxHealth, false);
    }

 

}
