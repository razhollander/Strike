using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour, ISceneObject
{
    const string SPAWN_ANIMATION_NAME = "Spawn";

    [SerializeField] private float _explosiveForce = 100;
    [SerializeField] private float _explosionRadius = 1000;
    [SerializeField] private float _upForce;
    [SerializeField] protected BaseVehicle _baseVehicle;
    [SerializeField] public GameObject Radius;
    [SerializeField] protected Animator _animator;

    private Vector3 _startPos;
    
    protected virtual void Awake()
    {
        _startPos = transform.position;
    }
    public abstract void AddForce(Vector2 force);
    private void Start()
    {
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnLeave += SpawnAnimtion;
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += HandleEnterNormalPlay;
    }

    protected virtual void HandleEnterNormalPlay()
    {
        _baseVehicle.enabled = true;
    }

    protected virtual void SpawnAnimtion()
    {
        transform.position = _startPos;
        _baseVehicle.enabled = false;
        _animator.SetTrigger(SPAWN_ANIMATION_NAME);
    }

    public void DoQuitAnimation()
    {
        var rigidBodies = GameObject.FindObjectsOfType<Rigidbody>();
        Vector3 pos = transform.position;
        foreach (var rb in rigidBodies)
        {
            rb.AddExplosionForce(_explosiveForce, pos, _explosionRadius, _upForce, ForceMode.Impulse);
        }
    }
}
