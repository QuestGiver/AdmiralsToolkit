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

    public override void SetAccelerationRequest()
    {
        CurrentAccelerationRequest.Velocity = (Brain.transform.position - goalDestination.position) * strength;
        CurrentAccelerationRequest.Priority = priority;
    }
}
