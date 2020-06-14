using System;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BendingManager : MonoBehaviour
{
    #region Constants

    public const string BENDING_FEATURE = "_ENABLE_BENDING";

    public static readonly int BENDING_AMOUNT =
      Shader.PropertyToID("_BendingAmount");

    #endregion


    #region Inspector

    [SerializeField]
    private bool enableBendInEditor = false;
    [SerializeField]
    [Range(0.000f, 0.1f)]
    private float bendingAmount = 0.003f;

    #endregion


    #region Fields

    public static event Action<float> OnBendingAmountChanged;

    private float _prevAmount;

    #endregion


    #region MonoBehaviour

    void Awake()
    {

        if (Application.isPlaying)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
            Debug.Log("Enable");
        }
        else
        {
            Shader.EnableKeyword(BENDING_FEATURE);

            //Shader.DisableKeyword(BENDING_FEATURE);
            Debug.Log("Disable");
        }

        UpdateBendingAmount();
        //base.Awake();
    }

    private void OnEnable()
    {

        if (!Application.isPlaying /*&& !enableBendInEditor*/)
            return;

        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    public void Update()
    {
        if (Math.Abs(_prevAmount - bendingAmount) > Mathf.Epsilon)
            UpdateBendingAmount();
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    #endregion


    #region Methods

    private void UpdateBendingAmount()
    {
        _prevAmount = bendingAmount;
        Shader.SetGlobalFloat(BENDING_AMOUNT, bendingAmount);
        OnBendingAmountChanged?.Invoke(bendingAmount);
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext ctx,
                                                Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99, 99, -99, 99, 0.001f, 99) *
                            cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering(ScriptableRenderContext ctx,
                                              Camera cam)
    {
        cam.ResetCullingMatrix();
    }

    #endregion
}