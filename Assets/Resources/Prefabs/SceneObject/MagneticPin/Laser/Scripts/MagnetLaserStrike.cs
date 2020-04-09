using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetLaserStrike : PooledMonobehaviour
{
    [SerializeField] LineRenderer _lineRenderer1;
    [SerializeField] LineRenderer _lineRenderer2;

    const float HALF = 0.5f;
    const float ZERO = 0;

    [SerializeField] public Transform Target;
    [SerializeField] private int _numberOfPoints = 40;
    [SerializeField] private float _length = 50;
    [SerializeField] private float _waveHeight = 4;
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _ringWidth = 3;
    [Range(0, 2)]
    [SerializeField] private float tolerance = 0.1f;

    private float delta = 0;
    private List<Vector3> points1 = new List<Vector3>(); // Generated points before Simplify is used.
    private List<Vector3> points2 = new List<Vector3>(); // Generated points before Simplify is used.

    float halfWaveHeight;
    float step;
    float pointY1;
    float pointY2;

    // Generates the sine wave points.
    public void GeneratePoints()
    {
        points1.Clear();
        points2.Clear();

        delta += Time.deltaTime * _moveSpeed;
        halfWaveHeight = _waveHeight * HALF;
        _length = (Target.position - transform.position).magnitude;
        step = _length / _numberOfPoints;

        for (int i = 0; i < _numberOfPoints; ++i)
        {
            pointY1 = Mathf.Sin((i * step + delta) / _ringWidth + Mathf.PI) * halfWaveHeight;
            pointY2 = Mathf.Sin((i * step + delta) / _ringWidth) * halfWaveHeight;
            points1.Add(new Vector3(i * step, pointY1, ZERO));
            points2.Add(new Vector3(i * step, pointY2, ZERO));
        }

        _lineRenderer1.positionCount = _numberOfPoints;
        _lineRenderer2.positionCount = _numberOfPoints;

        _lineRenderer1.SetPositions(points1.ToArray());
        _lineRenderer2.SetPositions(points2.ToArray());
    }

    public void SetAlpha(float alpha)//0<alpha<255
    {
        SetLineRendererAlpha(_lineRenderer1, alpha);
        SetLineRendererAlpha(_lineRenderer2, alpha);
    }

    private void SetLineRendererAlpha(LineRenderer lr, float alpha)
    {
        Gradient gradient = new Gradient();

        gradient.SetKeys( lr.colorGradient.colorKeys,
             new GradientAlphaKey[]
        {
            new GradientAlphaKey(alpha, 0.0f),
            new GradientAlphaKey(alpha, 1f)
        });

        lr.colorGradient = gradient;
    }

    public void Update()
{
    if (Target != null)
    {
        GeneratePoints();
        _lineRenderer1.Simplify(tolerance);
        _lineRenderer2.Simplify(tolerance);
    }
}
}
