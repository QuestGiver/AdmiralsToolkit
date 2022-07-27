using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockCentering : UrgeBehavior
{
    public float IdealDistance = 5f;
    [SerializeField] float normalizedDifference;
    [SerializeField] float totalStrength;

    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }


    public override void SetAccelerationRequest()
    {
        if (Brain.NieghborhoodBoids.Count > 0)
        {
            float _boidDistance = Vector3.Distance(Brain.transform.position, Brain.NieghborhoodCentroid);

            normalizedDifference = ((_boidDistance) / (IdealDistance));
            totalStrength = (strength * (normalizedDifference));

            CurrentAccelerationRequest.Velocity = (Brain.transform.position - Brain.NieghborhoodCentroid).normalized * totalStrength;
            CurrentAccelerationRequest.Priority = priority;


        }
        else
        {
            CurrentAccelerationRequest.Velocity = Vector3.zero;
            CurrentAccelerationRequest.Priority = 0;
        }

    }
}
