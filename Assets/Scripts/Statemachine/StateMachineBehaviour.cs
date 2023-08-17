using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineBehaviour : MonoBehaviour
{
    [SerializeField] private State initialState;

    [Header("Debugging")]
    [SerializeField] private State currentState;
    [SerializeField] private List<State> states;

    private void Awake()
    {
        this.InitStateMachine();
        StateMachine.SetState(initialState, out currentState);
    }

    public void UpdateStatesList()
    {
        if (StateMachine.States.Count > states.Count)
            states = StateMachine.States;
    }

    public void SetState(State state)
    {
        StateMachine.SetState(state, out currentState);
    }

    public void DoTheFunny()
    {
        print("Funny.");
    }
}
