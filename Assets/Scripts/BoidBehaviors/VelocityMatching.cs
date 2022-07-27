using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : UrgeBehavior
{
    [Tooltip("Objects within this radius effect this boids velocity")]
    [SerializeField] float _detectionRadius;
    
    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }
    
    public override void SetAccelerationRequest()
    {
        CurrentAccelerationRequest.Priority = priority;
        CurrentAccelerationRequest.Velocity = AverageNeighborVelocity().normalized * strength;
    }

    public Vector3 AverageNeighborVelocity()
    {
        Vector3 averageVelocity = Vector3.zero; //start with zero
        foreach (GameObject boid in Brain.LocalGroupBoids) //for each boid in the flock
        {
            averageVelocity += boid.GetComponent<Boid>().Velocity * DampenStrengthOverDistance(boid);// avergae velocity is set to itself plus the velocity of the current boid for this iteration
        }
        averageVelocity /= Brain.LocalGroupBoids.Count;
        return averageVelocity;
    }

    float DampenStrengthOverDistance(GameObject _boid)
    {
        float _boidDistance = Vector3.Distance(Brain.transform.position, _boid.transform.position);
        float normalizedDifference = 1 - ((_boidDistance) / (_detectionRadius));
        return normalizedDifference;
    }

}
