﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShooterVacuum : Vacuum
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float maxDistance=1000;
    [SerializeField] private float arrowLengthProportion = 1;
    [SerializeField] private float rayRadius = 0.5f;
    [SerializeField] private float arrowLerpAmount;
    SpriteRenderer arrowSpriteRenderer;
    float arrowDefaultHeight;
    RaycastHit rayhit;
    // Start is called before the first frame update
    public void StartAiming(Vector2 aimDirection)
    {
        if(ObjectBeingSucked != null && !isInPulling)
             StopSuckingEnemy();
        else
            rotationTweener.Kill();
        arrow.SetActive(true);
        Aim(aimDirection);

    }
    private void Start()
    {
        arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();
        arrowDefaultHeight = arrowSpriteRenderer.size.y;
    }
    public void Aim(Vector2 aimDirection)
    {
        Ray ray = new Ray(vacuumPoint.position, new Vector3(aimDirection.x, 0, aimDirection.y));
        Debug.DrawRay(vacuumPoint.position,  new Vector3(aimDirection.x, 0, aimDirection.y)*maxDistance, Color.red);
        transform.LookAt(transform.position + new Vector3(ray.direction.x,0, ray.direction.z), Vector3.up);
        RaycastHit rayhit;
        if (Physics.SphereCast(ray, rayRadius, out rayhit, maxDistance, LayerMask.GetMask("Enemy")))
        {
            arrowSpriteRenderer.size = Vector2.Lerp(arrowSpriteRenderer.size,new Vector2(arrowSpriteRenderer.size.x, rayhit.distance* arrowLengthProportion), arrowLerpAmount);
            arrowSpriteRenderer.color = Color.gray;

        }
        else
        {
            arrowSpriteRenderer.color = Color.white;
            arrowSpriteRenderer.size = Vector2.Lerp(arrowSpriteRenderer.size, new Vector2(arrowSpriteRenderer.size.x, arrowDefaultHeight), arrowLerpAmount); 
        }
    }
    public void Shoot(ObjectShot objectShot, Vector2 direction)
    {
        objectShot.transform.position = vacuumPoint.position;
        objectShot.Shoot(direction);
        StopAiming();
    }
    private void DoShootFX()
    {
        //airParticals.SetActive(false);
        //sparksParticles.Play();
        //swallowAnimationDuration = sparksParticles.main.startLifetime.constantMax;
        //vacuumHead.DOPunchRotation((vacuumHead.right + vacuumHead.forward) * 10, swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    public void StopAiming()
    {
        arrow.SetActive(false);
        StartSelfRotation();

    }
    // Update is called once per frame
}
