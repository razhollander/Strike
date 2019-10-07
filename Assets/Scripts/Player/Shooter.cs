using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartAiming()
    {
        arrow.SetActive(true);
    }
    public void Aim(Vector2 aimDirection)
    {
    }
    public void Shoot(GameObject objectShot, Vector2 direction)
    {
        Debug.Log("Shoot");
        StopAiming();
    }
    public void StopAiming()
    {
        arrow.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
