using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    public State CurrentState { get; private set; }
    public State UpgradesShop;
    public State MainMenu;
    public State NormalPlay;
    public State Login;
    public GameStateManager()
    {        
        UpgradesShop = new State();
        MainMenu = new State();
        NormalPlay = new State();
        Login = new State();

        CurrentState = Login;
        SwitchGameState(MainMenu);
    }
    public void SwitchGameState(State newState)
    {
        CurrentState.Leave();
        CurrentState = newState;
        CurrentState.Enter();
    }
    
}

public class State
{
    public event Action OnLeave;
    public event Action OnEnter;

    internal void Leave()
    {
        OnLeave?.Invoke();
    }
    internal void Enter()
    {
        OnEnter?.Invoke();
    }
}