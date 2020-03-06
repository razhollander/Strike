using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    const string UPDRAGE_STORE = "UpdrageStore";

    [SerializeField] Animator _animator;
    [SerializeField] Camera _mainCamera;
    [SerializeField] FollowCamera _followCamera;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.GameStateManager.GoToLocation += HandleNewState;
    }

    private void HandleNewState(eGameState gameState)
    {
        switch(gameState)
        {
            case eGameState.MainMenu:
                _followCamera.enabled = true;
                _animator.SetBool(UPDRAGE_STORE, false);
                break;
            case eGameState.UpgradesShop:
                _animator.SetBool(UPDRAGE_STORE, true);
                break;
            case eGameState.NormalPlay: 
                _followCamera.enabled = true; 
                break;
        }
    }
}
