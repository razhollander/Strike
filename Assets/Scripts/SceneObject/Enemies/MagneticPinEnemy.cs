using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPinEnemy : Enemy
{
    const float ZERO = 0; 
    const float ONE = 1;
    const float MAX_DISTANCE_PERCENT = 2;

    [Header("MagnecticPin")]
    [SerializeField] private ParticleSystem _attackEffect;
    [SerializeField] Transform _magnet;
    [SerializeField] private float _pullAmount;
    [SerializeField] private float _radius = 20;
    [SerializeField] MagnetLaserStrike _magnetLaserStrike;

    PlayerBase _player;
    Transform _target;

    float _lazerAlpha;
    float _distance = float.MaxValue;
    float _prevDistance = float.MaxValue;
    bool _isEffectEnabled = false;
    Vector3 towardsPin;
    public override SuckableObject Duplicate()
    {
        return this.Get<MagneticPinEnemy>();
    }
    protected override void Awake()
    {
        base.Awake();
        pulledEvent += StopEffect;
        startDyingEvent += StopEffect;
        pulledEvent += () => _isEffectEnabled = false;
        startDyingEvent += () => _isEffectEnabled = false;
    }
    private void Start()
    {
        _magnetLaserStrike.Target = GameManager.Instance.player.magneticForcePoint;
        _player = GameManager.Instance.player;
        _target = _player.magneticForcePoint;
    }
    private void Update()
    {
        if (_target != null&& _isEffectEnabled)
        {
            towardsPin = (transform.position - _target.position).SetYZero();
            _distance = towardsPin.magnitude;

            if (_distance < _radius)
            {
                if(_prevDistance>=_radius)
                {
                    StartEffect();
                }

                PullTarget();
                UpdateEffect();
            }
            else
            {
                if (_prevDistance < _radius)
                {
                    StopEffect();
                }
            }

            _prevDistance = _distance;
        }
    }

    private void StartEffect()
    {
        _attackEffect.Play();
    }
    private void UpdateEffect()
    {
        _magnet.LookAt(_target);
        _lazerAlpha = Mathf.Clamp(MAX_DISTANCE_PERCENT * (_radius - _distance) / _radius, ZERO, ONE);
        _magnetLaserStrike.SetAlpha(_lazerAlpha);
    }
    private void StopEffect()
    {
        _magnetLaserStrike.SetAlpha(ZERO);
        _attackEffect.Stop();
    }

    void PullTarget()
    {
        _player.AddForce(towardsPin.normalized * _pullAmount / _distance);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SpawnInDelay());
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        _magnet.gameObject.SetActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        _magnet.gameObject.SetActive(true);
        MakeActive(true);
    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        _magnetLaserStrike.gameObject.SetActive(isActive);
       _isEffectEnabled = isActive;

        if(!isActive)
        {
            _magnetLaserStrike.SetAlpha(ZERO);
        }

        base.MakeActive(isActive);
    }
    protected override void ResetTransform()
    {
        _magnet.localRotation = Quaternion.Euler(Vector3.zero);
        base.ResetTransform();
    }

}
