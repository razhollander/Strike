using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const string UPDRAGE_STORE_ANIMATION_NAME = "UpdrageStore";

    [SerializeField] Animator _animator;
    [SerializeField] Camera _mainCamera;
    [SerializeField] FollowCamera _followCamera;

    void Start()
    {
        GameManager.Instance.GameStateManager.MainMenu.OnEnter += () => {
            _followCamera.enabled = true;
            _animator.SetBool(UPDRAGE_STORE_ANIMATION_NAME, false); 
        };
        GameManager.Instance.GameStateManager.UpgradesShop.OnEnter += () => _animator.SetBool(UPDRAGE_STORE_ANIMATION_NAME, true);
        GameManager.Instance.GameStateManager.NormalPlay.OnEnter += () => _followCamera.enabled = true; 
    }
}
