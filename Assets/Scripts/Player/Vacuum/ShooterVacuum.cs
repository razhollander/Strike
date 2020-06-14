using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShooterVacuum : Vacuum
{
    [Header("Shooter")]

    [SerializeField] private Transform arrow;
    [SerializeField] private MeshRenderer arrowRenderer;
    [SerializeField] private MeshRenderer arrowHeadRenderer;

    [SerializeField] private float maxDistance=1000;
    [SerializeField] private float arrowLengthProportion = 1;
    [SerializeField] private float arrowHeadLength = 1;
    [SerializeField] private float rayRadius = 0.5f;
    [SerializeField] private float arrowLerpAmount;
    [SerializeField] private float punchShootValue = 1;
    [SerializeField] private float shootAnimationDuration=0.5f;

    bool isAiming=false;
    Ray aimRay;
    float arrowDefaultHeight;
    Tween shootTween;
    int enemyMask;
    GameObject arrowGO;
    Material arrowMat;
    public void StartAiming(Vector2 aimDirection)
    {
        if(ObjectBeingSucked != null && !IsInPulling)
             StopSuckingEnemy();
        RotationTweener.Kill();
        arrowGO.SetActive(true);
        isAiming = true;
        Aim(aimDirection);
    }
    public void SetArrow(bool isActive)
    {
        arrowGO.SetActive(isActive);
    }
    public void SetArrow(ArrowObject arrowObject)
    {
        arrowRenderer.sharedMaterial.color = Color.white;
        arrowMat = arrowRenderer.sharedMaterial = arrowHeadRenderer.sharedMaterial = arrowObject.Mat;
        arrow.localScale = new Vector3(arrow.localScale.x, arrowObject.Width, arrow.localScale.z);
    }
    protected override void Awake()
    {
        base.Awake();
        aimRay = new Ray(VacuumPoint.position, Vector3.zero);
        arrowDefaultHeight = arrow.localScale.x;
        enemyMask = LayerMask.GetMask("Enemy");
        arrowGO = arrow.gameObject;
    }
    public void Aim(Vector2 aimDirection)
    {
        var aimVector3 = aimDirection.ToVector3();
        aimRay = new Ray(VacuumPoint.position, aimVector3);
        transform.LookAt(transform.position + aimVector3, Vector3.up);
        RaycastHit rayhit;
        var arrowScale = arrow.localScale;
        if (Physics.SphereCast(aimRay, rayRadius, out rayhit, maxDistance, enemyMask))
        {
            arrow.localScale = Vector3.Lerp(arrowScale, new Vector3((rayhit.distance - arrowHeadLength) * arrowLengthProportion, arrowScale.y, arrowScale.z), arrowLerpAmount);
            arrowMat.color = Color.gray;
        }
        else
        {
            arrowMat.color = Color.white;
            arrow.localScale = Vector3.Lerp(arrowScale, new Vector3(arrowDefaultHeight, arrowScale.y, arrowScale.z), arrowLerpAmount); 
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
        arrowGO.SetActive(false);
        isAiming = false;
        if (!IsInPulling)
        StartSelfRotation();

    }
}
