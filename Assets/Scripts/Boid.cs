using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [SerializeField] int NieghborhoodSizeLimmit;
    [SerializeField] float NieghborhoodArea;
    [SerializeField] List<GameObject> NieghborhoodBoids;
    [SerializeField] Vector3 NieghborhoodCentroid;

    [SerializeField] float maximumAcceleration = 5;//this isn't really just a speed limmit, it's a rescource. A "portion" of the maximumAcceleration is given out to acceleration requests until there is none left, ensuring there is less indicision in the swarm.
    [SerializeField] Vector3 Velocity;

    [SerializeField] List<UrgeBehavior> urgeBehaviors;//The list of behaviors that currently contribute acceleration requests.
    List<AccelerationRequest> accelerationRequests = new List<AccelerationRequest>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (UrgeBehavior boid in urgeBehaviors)
        {
            boid.Brain = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResolveUrges();
        transform.position = transform.position - Velocity;
    }

    //Behavior Implamentation
    void ResolveUrges()
    {
        ProcessAccelerationRequests();
        PrioritizeRequests();
        float totalAcceleration = 0;

        foreach (AccelerationRequest request in accelerationRequests)
        {
            float requestedAcceleration = (request.Velocity * request.Priority).magnitude;
            if ((totalAcceleration + requestedAcceleration) < maximumAcceleration)
            {
                totalAcceleration += requestedAcceleration;
            }
            else
            {
                break;
            }
            Velocity += request.Velocity * request.Priority;
        }

        if (totalAcceleration < maximumAcceleration)
        {
            Velocity = Velocity * (maximumAcceleration - totalAcceleration);
        }

        Velocity = Velocity * Time.deltaTime;
    }

    void ProcessAccelerationRequests()
    {
        if (accelerationRequests.Count > 0)
        {
            accelerationRequests.Clear();
        }

        foreach (UrgeBehavior urge in urgeBehaviors)
        {
            urge.GenerateAccelerationRequest();
            accelerationRequests.Add(urge.CurrentAccelerationRequest);
        }
    }


    void PrioritizeRequests()
    {
        //Sort accelerationRequests by priority
        accelerationRequests.Sort((x, y) => x.Priority.CompareTo(y.Priority));
    }


}
