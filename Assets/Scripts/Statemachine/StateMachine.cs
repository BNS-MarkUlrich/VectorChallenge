using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateMachine
{
    private static List<State> states = new List<State>();
    private static State currentState;
    
    public static StateMachineBehaviour OwningStateMachine { get; private set; }

    public static List<State> States => states;

    public static void Subscribe(this State state)
    {
        if (states.Contains(state)) return;
        states.Add(state);
    }

    public static void InitStateMachine(this StateMachineBehaviour stateMachineBehaviour)
    {
        OwningStateMachine = stateMachineBehaviour;
    }

    public static void SetState(State state, out State newState)
    {
        if (currentState != null && currentState.enabled)
            currentState.enabled = false;
        
        currentState = state;
        newState = currentState;
        newState.enabled = true;
    }
}
