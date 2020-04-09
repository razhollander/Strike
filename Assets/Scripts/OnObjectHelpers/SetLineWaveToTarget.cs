using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetLineWaveToTarget : MonoBehaviour
{
    const float HALF = 0.5f;
    const float ZERO = 0;

    [SerializeField] private Transform _target;
    [SerializeField] private int _numberOfPoints = 40;
    [SerializeField] private float _length = 50;
    [SerializeField] private float _waveHeight = 4;
    [SerializeField] private bool _isOpposite = false;
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _ringWidth = 3;
    [Range(0,2)]
    [SerializeField] private float tolerance = 0.1f;

    private float delta=0;
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>(); // Generated points before Simplify is used.
    float halfWaveHeight;
    float step;
    float pointY;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Generates the sine wave points.
    public void GeneratePoints()
    {
        points.Clear();
        delta += Time.deltaTime * _moveSpeed;
        halfWaveHeight = _waveHeight * HALF;
        _length = (_target.position - transform.position).magnitude;
        step = _length / _numberOfPoints;
        
        for (int i = 0; i < _numberOfPoints; ++i)
        {  
            pointY = _isOpposite ? Mathf.Sin((i * step +  delta)/ _ringWidth + Mathf.PI) * halfWaveHeight : Mathf.Sin((i * step+ delta)/ _ringWidth) * halfWaveHeight;
            points.Add(new Vector3(i * step, pointY, ZERO));
        }

        lineRenderer.positionCount = _numberOfPoints;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void Update()
    {
        GeneratePoints();
        lineRenderer.Simplify(tolerance);
    }
}
