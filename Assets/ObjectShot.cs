using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class ObjectShot : MonoBehaviour
{
    public float damage;
    public float speed=10;
    protected float scaleUpTime = 0.5f;
    Tween rotationTweener;
    private void OnEnable()
    {
        
    }
    public void Shoot(Vector2 direction)
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.DOScale(1, scaleUpTime);
        DoSelfRotate();
        GetComponent<Rigidbody>().AddForce(new Vector3(direction.x, 0, direction.y) * speed, ForceMode.VelocityChange);

    }
    protected void DoSelfRotate()
    {
        rotationTweener= transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);

    }
    private void OnCollisionEnter(Collision collision)
    {
        rotationTweener.Kill();
    }
    private void OnDisable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
