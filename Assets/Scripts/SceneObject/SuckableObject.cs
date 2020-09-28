using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public abstract class SuckableObject : PooledMonobehaviour, ISceneObject
{
    private int DISTANCE_PROPERTY = Shader.PropertyToID("_Distance");
    private int OVER_DISTANCE_SUCKED_PROPERTY = Shader.PropertyToID("_OverDIstanceSucked");

    private int IS_SUCKED_PROPERTY = Shader.PropertyToID("_IsSucked");
    private int HOLE_POSITION_PROPERTY = Shader.PropertyToID("_HolePosition");

    private const float EXTRA_Y_SPACE = 0.1f;
    [SerializeField]
    protected bool canBeSucked = true;
    const float ON_QUIT_ANIMATION_TIME = 2f;

    [SerializeField] protected SuckableobjectType suckableobjectType;
    [SerializeField] public Renderer _renderer;
    [SerializeField] protected Collider thisCollider;
    [SerializeField] protected Rigidbody thisRigidBody;
    protected Material _material;

    private Vector3 BeginLocalScale;
    protected event Action pulledEvent;

    private bool _isSupportSuckEffect = false;
    public bool IsActive { get; private set; }
    public bool IsBeingSucked { get; set; }
    public bool CanBeSucked()
    {
        return canBeSucked;
    }

    protected virtual void Awake()
    {
        BeginLocalScale = transform.localScale;
        thisRigidBody.centerOfMass = Vector3.zero;
        pulledEvent += DisableCollider;

        _material = _renderer.material;
        _isSupportSuckEffect = _material.HasProperty(DISTANCE_PROPERTY) &&
            _material.HasProperty(HOLE_POSITION_PROPERTY) &&
            _material.HasProperty(IS_SUCKED_PROPERTY);

        if (_isSupportSuckEffect)
        {
            _material.SetInt(IS_SUCKED_PROPERTY, 0);
            //_material.SetFloat(DISTANCE_PROPERTY, GameManager.Instance.UpgradesManager.GetUpgrade<RadiusUpgrader>().GetUpgradeValue());
        }
    }

    public void DisableCollider()
    {
        thisCollider.enabled = false;
    }
    public void GetPulled(float startDistance)
    {
        if(_isSupportSuckEffect)
        {
            _material.SetInt(IS_SUCKED_PROPERTY, 1);
            _material.SetFloat(DISTANCE_PROPERTY, startDistance/2);
            _material.SetFloat(OVER_DISTANCE_SUCKED_PROPERTY, startDistance/2);
        }

        thisRigidBody.isKinematic = true;
        pulledEvent();
    }

    public virtual void Collected()
    {
        if (_isSupportSuckEffect)
        {
            _material.SetInt(IS_SUCKED_PROPERTY, 0);
        }

        gameObject.SetActive(false);
        IsBeingSucked = false;
    }

    public void SetSuckEffectPoint(Vector3 holePos)
    {
        if(_isSupportSuckEffect)
        {
            _material.SetVector(HOLE_POSITION_PROPERTY ,holePos);
        }
    }

    protected virtual void OnEnable()
    {
        ResetTransform();
        MakeActive(true);
    }
    protected override void OnDisable()
    {
        MakeActive(false);
        base.OnDisable();
    }
    protected virtual void ResetTransform()
    {
        Vector3 vectorZero = Vector3.zero;
        transform.localScale = BeginLocalScale;
        transform.localRotation = Quaternion.Euler(vectorZero);
        thisRigidBody.isKinematic = false;
        thisRigidBody.velocity = vectorZero;
        thisRigidBody.angularVelocity = vectorZero;

    }
    protected virtual void MakeActive(bool isActive)
    {
        thisCollider.enabled = isActive;
        _renderer.enabled = isActive;
        thisRigidBody.useGravity = isActive;
        IsActive = isActive;
    }

    public virtual SuckableObject Duplicate()
    {
        return this.Get<SuckableObject>(false);
    }

    public virtual void SetSpawnedPosition(Vector3 spawnedPos)
    {
        Vector3 yPos = (MeshHandler.GetMeshHeight(_renderer) / 2 + EXTRA_Y_SPACE) * Vector3.up;
        transform.position = spawnedPos + yPos;
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
    superBowlingBall = 6,
    powerUp = 7,
    coin=8
}
