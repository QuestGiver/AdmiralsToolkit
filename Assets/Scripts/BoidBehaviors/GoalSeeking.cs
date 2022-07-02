using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSeeking : UrgeBehavior
{
    [SerializeField]Transform goalDestination;

    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }

    public override void GenerateAccelerationRequest()
    {
        CurrentAccelerationRequest.Velocity = (Brain.transform.position - goalDestination.position).normalized;
        CurrentAccelerationRequest.Priority = 0.25f;
    }
}
