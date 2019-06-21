using UnityEngine;
using System.Collections;

public class StateManager<T> {
    
    public T Owner { get; private set; }

    public State<T> GlobalState { get; set; }
    public State<T> CurrentState { get; private set;}
    public State<T> LastState { get; private set;}

    public StateManager(T owner)
    {
        Owner = owner;
    }

    public void Update()
    {
        if (GlobalState != null)
            GlobalState.Execute(Owner);

        if (CurrentState != null)
            CurrentState.Execute(Owner);
    }

    public void ChangeState(State<T> newState)
    {

        if(CurrentState != null && CurrentState != newState)
        {
            CurrentState.OnExit(Owner);
            LastState = CurrentState;
        }

        if (newState != null)
        {
            CurrentState = newState;
            CurrentState.OnEnter(Owner);
        }
    }

    public void RevertToLastState()
    {
        ChangeState(LastState);
    }

    public bool IsInState(string stateName)
    {
        if (CurrentState == null)
            return false;

        return (CurrentState.Name.Equals(stateName));
    }
}
