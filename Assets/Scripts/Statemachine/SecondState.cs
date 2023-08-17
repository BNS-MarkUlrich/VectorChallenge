using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondState : State
{
    protected override void EnterState()
    {
        print("Hello! I am the Second State");
    }

    protected override void ExitState()
    {
        OwningStateMachine.DoTheFunny();
        print("Bye! I was the Second State");
    }
}
