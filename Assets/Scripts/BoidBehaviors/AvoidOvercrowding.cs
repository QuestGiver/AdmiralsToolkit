using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidOvercrowding : UrgeBehavior
{
    public float MinimumDistance = 0.25f;

    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }

    public override void SetAccelerationRequest()
    {
        if (Vector3.Distance(Brain.transform.position, Brain.NieghborhoodCentroid) < MinimumDistance)
        {
            
            CurrentAccelerationRequest.Velocity = (Brain.transform.position + Brain.NieghborhoodCentroid).normalized * strength;
            CurrentAccelerationRequest.Priority = priority;
        }
    }
}
