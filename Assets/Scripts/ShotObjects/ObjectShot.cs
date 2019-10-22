using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ObjectShot : PooledMonobehaviour
{
    [Header("ObjectShot")]
    [SerializeField] protected float damage;
    [SerializeField] private ParticleSystem destroyFX;
    [SerializeField] protected Renderer myRenderer;
    [SerializeField] private Collider myCollider;
    [SerializeField] protected Rigidbody myRigidbody;
    public float speed = 10;

    protected delegate IEnumerator OnCollisionDelegate(Enemy enemy);
    protected OnCollisionDelegate onCollisionFunc;
    protected float scaleUpTime = 0.5f;

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
            if (onCollisionFunc != null)
                StartCoroutine(onCollisionFunc(enemy));
            else
                StartCoroutine(DestroySelf());
        }
    }
    protected IEnumerator DestroySelf(float delay = 0)
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
        //Renderer[] rendererArr =  GetComponentsInChildren<Renderer>();
        //for (int i = 0; i < rendererArr.Length; i++)
        //{
        //    rendererArr[i].enabled = isEnabled;
        //}
        myRenderer.enabled = isEnabled;
        myCollider.enabled = isEnabled;
        myRigidbody.isKinematic = !isEnabled;
    }
    private void Reset()
    {

    }
}
