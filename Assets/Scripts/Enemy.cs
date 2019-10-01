using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    Sequence mySeq;
    public float speed = 1;
    public float health;
    public Player player;
    private Coroutine healthCoroutine;
    private Tweener shakeTweener;
    private int maxHealth=100;
    public SimpleHealthBar healthBar;
    public bool isInVaccumRange = false;
    public bool isBeingSucked { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        player = FindObjectOfType<Player>();
        //mySeq = DOTween.Sequence();
        //mySeq.Append(transform.DOMove(player.transform.position, 3));
        //mySeq.Insert(0,transform.DOLocalRotate(new Vector3(0, 1000, 1000),1,RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart));
        //mySeq.Append(transform.DOLocalRotate(new Vector3(90, 0, 0), 0.5f, RotateMode.LocalAxisAdd));

        //mySeq.Append(transform.DOLocalRotate(new Vector3(0, 1000, 0), 1, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Restart));
        //mySeq.Insert(0,transform.DOBlendableLocalRotateBy(new Vector3(90, 0, 0), 0.5f, RotateMode.WorldAxisAdd));
        //StartCoroutine(GetSucked());
        //DOTween.To(() => transform.position, x => transform.position = x, player.VaccumPoint.position, 10);

        //transform.DOMove(player.transform.position,2).SetEase(Ease.InBounce).SetLoops(4).OnComplete(Func);

        //StartInVaccumRange();
    }
    public void StartInVaccumRange()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        shakeTweener= transform.DOShakeRotation(4,10,6,70,false).SetEase(Ease.Linear).SetLoops(-1);
        healthCoroutine = StartCoroutine(LoseHealth());
    }
    public void EndInVaccumRange()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        shakeTweener.Restart();
        shakeTweener.Kill();
        StopCoroutine(healthCoroutine);
        SetHealth(maxHealth,false);
    }
    private void SetHealth(int value, bool isRelative=true)
    {
        health = isRelative ? health + value : value;
        healthBar.UpdateBar(health, maxHealth);

    }
    private IEnumerator LoseHealth()
    {
        while(health>0)
        {
            SetHealth(-1, true);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(GetSucked());
    }
    IEnumerator GetSucked()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.None;
        shakeTweener.Kill();
        //rb.AddExplosionForce(1, transform.position, 1);
        //transform.DOShakeRotation(4);
        int x = Random.Range(500, 1000);
        int y = Random.Range(500, 1000);
        int z = Random.Range(500, 1000);
        int daruation = 4;
        transform.DOLocalRotate(new Vector3(x, y, z), daruation, RotateMode.LocalAxisAdd);
        transform.DOScale(0.01f, daruation);
        while (Vector3.Distance(transform.position,player.VaccumPoint.position)>1)
        {
            rb.AddForce((player.VaccumPoint.position - transform.position).normalized * speed);
            //transform.LookAt(player.VaccumPoint.position);
            yield return null;
        }
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Func()
    {
        Debug.Log("Hi");
    }
}
