using System;
using System.Collections.Generic;
using UnityEngine;


public class State<T>
{
    private static State<T> instance;

    public static State<T> Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new State<T>();
            }

            return instance;
        }
    }

    public string Name { get; protected set; }

    public State()
    {
    }

    public State(string name)
    {
        Name = name;
    }

    public virtual void OnEnter(T owner)
    {

    }
    
    public virtual void Execute(T owner)
    {

    }
    
    public virtual void OnExit(T owner)
    {

    }
}
