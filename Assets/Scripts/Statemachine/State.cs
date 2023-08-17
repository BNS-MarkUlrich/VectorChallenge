using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    protected StateMachineBehaviour OwningStateMachine;

    [SerializeField] protected UnityEvent onStateEnter;
    [SerializeField] protected UnityEvent onStateExit;

    private void OnEnable()
    {
        InitState();
        
        EnterState();
        onStateEnter?.Invoke();
    }

    private void OnDisable()
    {
        onStateExit?.Invoke();
        ExitState();
    }

    protected void InitState()
    {
        this.Subscribe();
        OwningStateMachine = StateMachine.OwningStateMachine;
        OwningStateMachine.UpdateStatesList();
    }

    protected abstract void EnterState();
    
    protected abstract void ExitState();
}
