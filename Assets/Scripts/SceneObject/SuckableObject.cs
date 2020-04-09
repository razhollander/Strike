using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class SuckableObject : PooledMonobehaviour, ISceneObject
{
    const float ON_QUIT_ANIMATION_TIME = 2f;

    [SerializeField] protected SuckableobjectType suckableobjectType;
    [SerializeField] int scoreValue;
    [SerializeField] public Renderer thisRenderer;
    [SerializeField] protected Collider thisCollider;
    [SerializeField] protected Rigidbody thisRigidBody;

    [System.NonSerialized] private bool isBeingSucked = false;
    private Vector3 BeginLocalScale;
    protected event Action pulledEvent;
    public bool IsBeingSucked { get => isBeingSucked; set => isBeingSucked = value; }

    public void DisableCollider()
    {
        thisCollider.enabled = false;
    }
    public void GetPulled()
    {
        pulledEvent();
    }
    public void Collected()
    {
        InventoryUI.instance.StartAddEffect(suckableobjectType, transform.position);
        gameObject.SetActive(false);
        isBeingSucked = false;

    }
    protected virtual void Awake()
    {
        BeginLocalScale = transform.localScale;
        thisRigidBody.centerOfMass = Vector3.zero;
        pulledEvent += DisableCollider;
    }
    protected virtual void OnEnable()
    {
        ResetTransform();
        //GameManager.Instance.OnGamePlayEnd += DoQuitAnimation;
    }
    protected override void OnDisable()
    {
        //GameManager.Instance.OnGamePlayEnd -= DoQuitAnimation;
        base.OnDisable();
    }
    protected void ResetTransform()
    {
        transform.localScale = BeginLocalScale;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        thisRigidBody.velocity = Vector3.zero;
        //thisRigidBody.inertiaTensorRotation = Quaternion.Euler(0, 0, 0);
        //thisRigidBody.inertiaTensor = Vector3.zero;

        thisRigidBody.angularVelocity = Vector3.zero;
    }
    protected virtual void MakeActive(bool isActive)
    {
        thisCollider.enabled = isActive;
        thisRenderer.enabled = isActive;
    }

    public virtual SuckableObject Duplicate()
    {
        return this.Get<SuckableObject>();
    }

    public void DoQuitAnimation()
    {
        DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.zero, ON_QUIT_ANIMATION_TIME).onComplete+= () => gameObject.SetActive(false);
    }
    public void AddForce(Vector3 force)
    {
        thisRigidBody.AddForce(force);
    }
}
public enum SuckableobjectType
{
    bowlingBall = 0,
    normalPin = 1,
    firePin = 2,
    electricPin = 3,
    IcePin = 4,
    magneticPin = 5,
    powerUp = 6
}
