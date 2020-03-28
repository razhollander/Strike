using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleBehaviour;
public class Player : MonoBehaviour, ISceneObject
{
    private const float SpawnY = 11;

    [SerializeField] private float explosiveForce=100;
    [SerializeField] private float explosionRadius= 1000;
    [SerializeField] private float upForce;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float DownSpeed = 2;
    [SerializeField] private WheelVehicle WheelVehicle;
    [SerializeField] public GameObject Radius;
    [SerializeField] private Animator _animator;
    private Vector3 _startPos;
    void Awake()
    {
        _startPos = transform.position;
    }
    private void Start()
    {
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnLeave += SpawnAnimtion;
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += () =>
        {
            _animator.enabled = false;
            WheelVehicle.enabled = true;
        };
    }
    void SpawnAnimtion()
    {
        Debug.Log("Animation");
        _animator.enabled = true;
        WheelVehicle.enabled = false;
        _animator.SetTrigger("Spawn");
        //_rigidbody.velocity = Vector3.down* DownSpeed;
        //transform.position = new Vector3(_startPos.x, _startPos.y + SpawnY, _startPos.z);
    }

    public void DoQuitAnimation()
    {
        var rigidBodies = GameObject.FindObjectsOfType<Rigidbody>();
        Vector3 pos = transform.position;
        foreach (var rb in rigidBodies)
        {
            rb.AddExplosionForce(explosiveForce, pos, explosionRadius, upForce, ForceMode.Impulse);
        }
    }
}
