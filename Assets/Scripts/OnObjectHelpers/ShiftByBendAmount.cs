using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftByBendAmount : OverridableMonoBehaviour
{
    const float _two = 2;
    readonly Vector3 vDown = Vector3.down;

    private Transform _cam;
    private float deltaX = 0;
    private float deltaZ = 0;
    private float _BendAmount;
    bool _isBendingEnables = true;
    private Vector3 startLocalPos;
    protected override void Awake()
    {
        base.Awake();
        startLocalPos = transform.localPosition;
        _cam = Camera.main.transform;
        _BendAmount = Shader.GetGlobalFloat(BendingManager.BENDING_AMOUNT);

        BendingManager.OnBendingAmountChanged += SetNewBendAmount;
        _isBendingEnables = Shader.IsKeywordEnabled(BendingManager.BENDING_FEATURE);
    }

    private void SetNewBendAmount(float newBendingAmount)
    {
        _BendAmount = newBendingAmount;
    }

    private void OnDestroy()
    {
        BendingManager.OnBendingAmountChanged -= SetNewBendAmount;
    }
    public override void UpdateMe()
    {
        if (_isBendingEnables)
        {
            deltaX = transform.position.x - _cam.position.x;
            deltaX = Mathf.Pow(deltaX, _two);

            deltaZ = transform.position.z - _cam.position.z;
            deltaZ = Mathf.Pow(deltaZ, _two);

            GetComponent<RectTransform>().localPosition =startLocalPos - transform.InverseTransformDirection(transform.position + vDown * (_BendAmount * deltaZ + _BendAmount * deltaX));
        }

        //transform.LookAt(_cam);
    }
}
