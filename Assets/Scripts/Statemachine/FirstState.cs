using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstState : State
{
    [SerializeField] private GameObject myUI;
    
    protected override void EnterState()
    {
        print("Hello! I am the First State");
        myUI.SetActive(true);
    }

    protected override void ExitState()
    {
        print("Bye! I was the First State");
        myUI.SetActive(false);
    }
}
