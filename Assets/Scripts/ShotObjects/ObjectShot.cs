﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ObjectShot : PooledMonobehaviour, ISceneObject
{
    const float ON_QUIT_ANIMATION_TIME = 2f;

    [Header("ObjectShot")]
    [SerializeField] protected float damage;
    [SerializeField] private ParticleSystem destroyFX;
    [SerializeField] protected Renderer myRenderer;
    [SerializeField] private Collider myCollider;
    [SerializeField] protected Rigidbody myRigidbody;
    public float speed = 10;
    protected event Action<Enemy> OnCollision;
    protected float scaleUpTime = 0.5f;

    protected virtual void Awake()
    {
        OnCollision += HandleCollision;
    }
    protected virtual void OnEnable()
    {
        SetComponents(true);
        myRigidbody.velocity = Vector3.zero;
    }
    public void Shoot(Vector2 direction)
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(1, scaleUpTime);
        Vector3 dir = new Vector3(direction.x, 0, direction.y);
        transform.LookAt(transform.position+ dir, Vector3.up);
        GetComponent<Rigidbody>().AddForce(dir * speed, ForceMode.VelocityChange);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.collider.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            //if (OnCollision != null)
            //    OnCollision(enemy);
            //else
            //    StartCoroutine(DestroySelf());
            OnCollision?.Invoke(enemy);
        }
    }
    protected virtual void HandleCollision(Enemy enemy)
    {
        StartCoroutine(DestroySelf());
    }
    protected virtual IEnumerator DestroySelf(float delay = 0)
    {
        if (destroyFX != null)
        {
            delay = delay > destroyFX.main.duration ? delay : destroyFX.main.duration;
            destroyFX.Play();
        }
        SetComponents(false);
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    protected virtual void SetComponents(bool isEnabled)
    {
        myRenderer.enabled = isEnabled;
        myCollider.enabled = isEnabled;
        myRigidbody.isKinematic = !isEnabled;
    }

    public void DoQuitAnimation()
    {
        DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.zero, ON_QUIT_ANIMATION_TIME).onComplete += () => gameObject.SetActive(false);
    }
}
