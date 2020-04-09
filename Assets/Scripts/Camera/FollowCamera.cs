using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform target;

    [SerializeField] Vector3 offset;

    [SerializeField] float rotationOffset;
    // Camera speeds
    [Range(0, 10)]
    [SerializeField] float lerpPositionMultiplier = 1f;

    private void Start()
    {
        target = GameManager.Instance.player.transform;
    }
    //public override void FixedUpdateMe()
    //{
    //    Vector3 tPos = target.position + offset + target.forward * rotationOffset;
    //    transform.position = Vector3.Lerp(transform.position, tPos, Time.fixedDeltaTime * lerpPositionMultiplier);
    //}
    public void Update()
    {
        Vector3 tPos = target.position + offset + target.forward * rotationOffset;
        transform.position = Vector3.Lerp(transform.position, tPos, Time.deltaTime * lerpPositionMultiplier);
    }

}
