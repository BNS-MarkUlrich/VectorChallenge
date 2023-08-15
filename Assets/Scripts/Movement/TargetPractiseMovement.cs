using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TargetPractiseMovement : ZeroGMovement
{
    [SerializeField] private List<Transform> targets;

    private int currentTargetIndex;

    private void FollowPath()
    {
        if (HasReachedTarget())
        {
            currentTargetIndex++;
        }
        else
        {
            return;
        }

        if (currentTargetIndex >= targets.Count)
        {
            currentTargetIndex = 0;
        }
        
        Target = targets[currentTargetIndex];
    }

    private void Update()
    {
        FollowPath();
        MoveToTargetIgnoreAngle(Target);
    }
}
