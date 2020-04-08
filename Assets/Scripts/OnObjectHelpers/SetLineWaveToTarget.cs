using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetLineWaveToTarget : MonoBehaviour
{
    public Transform target;
    public int numberOfPoints = 1000;
    public float length = 50;
    public float waveHeight = 10;
    public bool isOpposite = false;
    [Range(0,2)]
    public float tolerance = 0.1f;

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>(); // Generated points before Simplify is used.

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Generates the sine wave points.
    public void GeneratePoints()
    {
        points.Clear();
        float halfWaveHeight = waveHeight * 0.5f;
        length = (target.position - transform.position).magnitude;
        float step = length / numberOfPoints;
        float y = 0;
        for (int i = 0; i < numberOfPoints; ++i)
        {
            y = isOpposite ? Mathf.Sin(i * step + Mathf.PI) * halfWaveHeight : Mathf.Sin(i * step) * halfWaveHeight;
            points.Add(new Vector3(i * step, y, 0));
        }
        lineRenderer.positionCount = numberOfPoints;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void Update()
    {
        GeneratePoints();
        lineRenderer.Simplify(tolerance);
    }
}
