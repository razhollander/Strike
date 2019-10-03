using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float health;
    private int maxHealth=100;
    public SimpleHealthBar healthBar;

    void Start()
    {
        health = maxHealth; 
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
    public void Collected()
    {
        Destroy(gameObject);
    }
 

}
