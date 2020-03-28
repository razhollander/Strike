using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{
    public State CurrentState { get; private set; }
    Dictionary<Type,State> States;

    public GameStateManager()
    {
        States = new Dictionary<Type, State>();
        States.Add(typeof(MainMenuState), new MainMenuState());
        States.Add(typeof(LoginState), new LoginState());
        States.Add(typeof(UpgradesShopState), new UpgradesShopState());
        States.Add(typeof(NormalPlayState), new NormalPlayState());

        CurrentState = GetState<LoginState>();
        SwitchGameState<MainMenuState>();
    }
    public T GetState<T>() where T : State
    {
        return (T)States[typeof(T)];
    }
    //public void AddOnEnter<T>(Action enterAction) where T : State
    //{
    //    States[typeof(T)].OnEnter += enterAction;
    //}
    //public void AddOnLeave<T>(Action leaveAction) where T : State
    //{
    //    States[typeof(T)].OnLeave += leaveAction;
    //}
    public void SwitchGameState<T>() where T : State
    {
        CurrentState.Leave();
        CurrentState = GetState<T>();
        CurrentState.Enter();
    }
}

public abstract class State
{
    public event Action OnLeave;
    public event Action OnEnter;

    public virtual void Leave()
    {
        OnLeave?.Invoke();
    }
    public virtual void Enter()
    {
        OnEnter?.Invoke();
    }
}