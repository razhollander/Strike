﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShooterVacuum : Vacuum
{
    [Header("Shooter")]

    [SerializeField] private GameObject arrow;
    [SerializeField] private float maxDistance=1000;
    [SerializeField] private float arrowLengthProportion = 1;
    [SerializeField] private float rayRadius = 0.5f;
    [SerializeField] private float arrowLerpAmount;
    [SerializeField] private float punchShootValue = 1;
    [SerializeField] private float shootAnimationDuration=0.5f;

    bool isAiming=false;
    SpriteRenderer arrowSpriteRenderer;
    float arrowDefaultHeight;
    RaycastHit rayhit;
    Tween shootTween;
    public void StartAiming(Vector2 aimDirection)
    {
        if(ObjectBeingSucked != null && !IsInPulling)
             StopSuckingEnemy();
        RotationTweener.Kill();
        arrow.SetActive(true);
        isAiming = true;
        Aim(aimDirection);
    }
    public void SetArrow(bool isActive)
    {
        arrow.SetActive(isActive);
    }
    public void SetArrow(ArrowObject arrowObject)
    {
        SpriteRenderer spriteRenderer = arrow.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = arrowObject.sprite;
        spriteRenderer.size = new Vector2(arrowObject.width, spriteRenderer.size.y);
        //if (arrowObject.isHasChildSprite)
        //    arrowObject.childSprite
    }
    protected override void Start()
    {
        base.Start();
        arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();
        arrowDefaultHeight = arrowSpriteRenderer.size.y;
    }
    public void Aim(Vector2 aimDirection)
    {
        Ray ray = new Ray(VacuumPoint.position, new Vector3(aimDirection.x, 0, aimDirection.y));
        Debug.DrawRay(VacuumPoint.position,  new Vector3(aimDirection.x, 0, aimDirection.y)*maxDistance, Color.red);
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
        objectShot.transform.position = VacuumPoint.position;
        objectShot.Shoot(direction);
        StopAiming();
        DoShootFX();
    }
    private void DoShootFX()
    {
        RotationTweener.Kill();
        shootTween.Restart();
        shootTween.Kill();
        shootTween = VacuumHead.DOPunchScale(Vector3.one * punchShootValue, shootAnimationDuration).OnComplete(StartSelfRotation);
        //airParticals.SetActive(false);
        //sparksParticles.Play();
        //swallowAnimationDuration = sparksParticles.main.startLifetime.constantMax;
        //vacuumHead.DOPunchRotation((vacuumHead.right + vacuumHead.forward) * 10, swallowAnimationDuration).OnComplete(EndSwallowAnimation);
    }
    protected override void StartSelfRotation()
    {
        if (!isAiming)
            base.StartSelfRotation();
    }
    public void StopAiming()
    {
        arrow.SetActive(false);
        isAiming = false;
        if (!IsInPulling)
        StartSelfRotation();

    }
}
