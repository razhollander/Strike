using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticPinEnemy : AttackingEnemy
{
    //const string SHADER_ALPHA_PARAMETER = "_Shader_Alpha";
    const float ZERO = 0; 
    const float ONE = 1;
    const float MAX_DISTANCE_PERCENT = 2;

    [Header("MagnecticPin")]
    [SerializeField] private ParticleSystem _attackEffect;
    [SerializeField] Transform _magnet;
    [SerializeField] private float _pullAmount;
    [SerializeField] private float _attackDauration = 3;
    //[SerializeField] private Material _pulledObjectMaterial;
    [SerializeField] private float _maxDistance = 10;
    [SerializeField] MagnetLaserStrike _magnetLaserStrike;

    PlayerBase _player;
    Transform _target;
    Coroutine _pullPlayerCoroutine;
    Coroutine _updateLaserCoroutine;

    float distance = float.MaxValue;
    Vector3 towardsPin;
    public override SuckableObject Duplicate()
    {
        return this.Get<MagneticPinEnemy>();
    }
    protected override void Awake()
    {
        base.Awake();
        pulledEvent += StopAttack;
        startDyingEvent += StopAttack;
    }
    private void Start()
    {
        _magnetLaserStrike._target = GameManager.Instance.player.magneticForcePoint;
        _player = GameManager.Instance.player;
        _target = _player.magneticForcePoint;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(SpawnInDelay());
        StartCoroutine(AttackCountdown());
    }
    private IEnumerator SpawnInDelay()
    {
        MakeActive(false);
        yield return new WaitForSeconds(spawnTimeDelay);
        MakeActive(true);
    }
    protected override void MakeActive(bool isActive)
    {
        thisRenderer.gameObject.SetActive(isActive);
        base.MakeActive(isActive);
    }
    protected override IEnumerator Attack()
    {
        _attackEffect.Play();
        _pullPlayerCoroutine = StartCoroutine(PullPlayer());
        _updateLaserCoroutine = StartCoroutine(UpdateLaserEffect());
        yield return new WaitForSeconds(_attackDauration);
        StopAttack();
    }
    private IEnumerator UpdateLaserEffect()
    {
        float alpha;

        while (true)
        {
            _magnet.LookAt(_target);
            
            if (distance < _maxDistance)
            {
                alpha = Mathf.Clamp(MAX_DISTANCE_PERCENT * (_maxDistance-distance)/_maxDistance,ZERO,ONE);
                _magnetLaserStrike.SetAlpha(alpha);
                //_pulledObjectMaterial.SetFloat(SHADER_ALPHA_PARAMETER, alpha);

            }
            else
            {
                _magnetLaserStrike.SetAlpha(ZERO);
                //_pulledObjectMaterial.SetFloat(SHADER_ALPHA_PARAMETER, ZERO);
            }

            yield return null;
        }
    }
    private IEnumerator PullPlayer()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            towardsPin = (transform.position- _target.position).SetYZero();
            distance = towardsPin.magnitude;
            if (distance < _maxDistance)
                _player.AddForce(towardsPin.ToVector2().normalized * _pullAmount/ distance);
        }
    }
    public override void StopAttack()
    {
        if (_pullPlayerCoroutine != null)
        {
            StopCoroutine(_pullPlayerCoroutine);
        }
        if(_updateLaserCoroutine!=null)
        {
            StopCoroutine(_updateLaserCoroutine);
        }

        _attackEffect.Stop();
        base.StopAttack();
    }
}
