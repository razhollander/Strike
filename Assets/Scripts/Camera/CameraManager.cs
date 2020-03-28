using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraManager : MonoBehaviour
{
    const string UPDRAGE_STORE_ANIMATION_NAME = "UpdrageStore";
    const string NORMAL_PLAY_ANIMATION_NAME = "NormalPlay";
    const float MOVE_TO_START_POS_TIME = 0.7f;

    [SerializeField] Animator _animator;
    [SerializeField] Camera _mainCamera;
    [SerializeField] FollowCamera _followCamera;

    Vector3 _startPos;
    Quaternion _startRot;
    private void Awake()
    {
        _startPos = transform.position;
        _startRot = transform.rotation;
    }
    void Start()
    {
        GameManager.Instance.GameStateManager.GetState<MainMenuState>().OnEnter += () =>
        {
            _animator.SetBool(UPDRAGE_STORE_ANIMATION_NAME, false);
            _animator.enabled = true;
        };
        GameManager.Instance.GameStateManager.GetState<UpgradesShopState>().OnEnter += () => _animator.SetBool(UPDRAGE_STORE_ANIMATION_NAME, true);
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnEnter += () =>
        {
            //_animator.SetBool(NORMAL_PLAY_ANIMATION_NAME, true);
            //_animator.enabled = false;
            _followCamera.enabled = true;
        };
        GameManager.Instance.GameStateManager.GetState<NormalPlayState>().OnLeave += () =>
        {
            _followCamera.enabled = false;
            MoveCameraToStartPosition();
        };

    }
    private void MoveCameraToStartPosition()
    {
        transform.DORotateQuaternion(_startRot, MOVE_TO_START_POS_TIME);
        transform.DOMove(_startPos, MOVE_TO_START_POS_TIME).OnComplete(() =>
          {
              //_animator.SetBool(UPDRAGE_STORE_ANIMATION_NAME, false);
              //_animator.SetBool(NORMAL_PLAY_ANIMATION_NAME, false);
          });
    }
}
