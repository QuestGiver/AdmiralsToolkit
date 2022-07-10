using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : UrgeBehavior
{

    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }

    public Vector3 AverageNeighborVelocity()
    {
        Vector3 averageVelocity = Vector3.zero; //start with zero
        foreach (GameObject boid in Brain.NieghborhoodBoids) //for each boid in the flock
        {
            averageVelocity += boid.GetComponent<Boid>().Velocity;// avergae velocity is set to itself plus the velocity of the current boid for this iteration
        }
        averageVelocity /= Brain.NieghborhoodBoids.Count;
        return averageVelocity;
    }
    
    public override void SetAccelerationRequest()
    {
        CurrentAccelerationRequest.Priority = priority;
        CurrentAccelerationRequest.Velocity = AverageNeighborVelocity().normalized * strength;
    }
}
