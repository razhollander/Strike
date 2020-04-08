using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetLineToTarget : MonoBehaviour
{

    private LineRenderer _lineRenderer;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _lineRenderer.SetPosition(1, transform.InverseTransformPoint(target.position));
    }
}
