using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ObjectShot : PooledMonobehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private ParticleSystem destroyFX;
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private Collider myCollider;
    [SerializeField] private Rigidbody myRigidbody;
    public float speed=10;

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
        GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 0, direction.y) * speed, ForceMode.VelocityChange);

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
    protected IEnumerator DestroySelf()
    {
        if(destroyFX!=null)
        {
            SetComponents(false);
            destroyFX.Play();
            yield return new WaitForSeconds(destroyFX.main.duration);
        }
        gameObject.SetActive(false);
    }
    private void SetComponents(bool isEnabled)
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
