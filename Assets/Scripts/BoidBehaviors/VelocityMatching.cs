using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatching : UrgeBehavior
{
    public List<GameObject> NeighboringBoids;

    public float AverageNeighborVelocity()
    {
        Vector3 averageVelocity = Vector3.zero; //start with zero
        foreach (GameObject boid in NeighboringBoids) //for each boid in the flock
        {
            averageVelocity += boid.GetComponent<Rigidbody>().velocity;// 
        }
        averageVelocity /= NeighboringBoids.Count;
        return averageVelocity.magnitude * strength;
    }

    public override void GenerateAccelerationRequest()
    {

    }
}
