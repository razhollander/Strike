using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    public eGameState CurrentState { get; private set;}
    public event Action GoToUpgradesShop;
    public event Action GoToMainMenu;
    public event Action GoToNormalPlay;
    public event Action GoToLogin;
    public event Action<eGameState> GoToLocation;
    public GameStateManager()
    {
        CurrentState = eGameState.Login;
    }

    public void SwitchGameState(eGameState newState)
    {
        GoToLocation?.Invoke(newState);

        switch (newState)
        {
            case eGameState.Login:
                    GoToLogin?.Invoke();
                break;
            case eGameState.MainMenu:
                    GoToMainMenu?.Invoke(); 
                break;
            case eGameState.UpgradesShop:
                    GoToUpgradesShop?.Invoke();
                break;
            case eGameState.NormalPlay:
                    GoToNormalPlay?.Invoke();
                break;
        }
    }
}
public enum eGameState
{
    Login,
    MainMenu,
    UpgradesShop,
    NormalPlay
}
