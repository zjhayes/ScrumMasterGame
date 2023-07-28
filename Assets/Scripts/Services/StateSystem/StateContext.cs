using System.Collections.Generic;
using UnityEngine;

public class StateContext<T> where T : IController
{
    public delegate void OnTransition();
    public OnTransition onTransition;

    public IState<T> CurrentState
    {
        get; set;
    }

    private readonly T controller;

    public StateContext(T _controller)
    {
        controller = _controller;
    }

    public void Transition()
    {
        CurrentState.Handle(controller);
        onTransition?.Invoke();
    }

    public void Transition<U>(U state) where U : Component, IState<T>
    {
        if (CurrentState != null)
        {
            CurrentState.Exit();
        }

        CurrentState = state;
        CurrentState.Handle(controller);
        onTransition?.Invoke();
    }
}