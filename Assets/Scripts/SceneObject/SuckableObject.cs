using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObject: PooledMonobehaviour
{
    [SerializeField]
    protected SuckableobjectType suckableobjectType;
    [System.NonSerialized] public bool isBeingSucked = false;
    private Vector3 BeginLocalScale;
    public void Collected()
    {

        InventoryUI.instance.StartAddEffect(suckableobjectType,transform.position);
        gameObject.SetActive(false);
        isBeingSucked = false;
        
    }
    private void Awake()
    {
        BeginLocalScale = transform.localScale;
    }
    protected virtual void OnEnable()
    {
        transform.localScale = BeginLocalScale;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
    public virtual SuckableObject Duplicate()
    {
        return this.Get<SuckableObject>();
    }

}
public enum SuckableobjectType
{
    bowlingBall=0,
    normalPin=1,
    firePin=2,
    electricPin = 3,
    IcePin=4,
    powerUp =5
}
