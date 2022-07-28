using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    //2/26/22
    //rework the boids to havea  flock ID so that you can search through the hierarchy to look
    //for a boid tag and then determine if the boids have the right flock ID to join the neighborhood.
    //This will remove the clunky AF collider system
    
    [SerializeField] public int NieghborhoodSizeLimmit;//number of nieghbors that a boid can register
    [SerializeField] public float NieghborhoodArea;
    [SerializeField] public List<GameObject> NieghborhoodBoids;
    [SerializeField] public List<GameObject> LocalGroupBoids;
    [SerializeField] public Vector3 NieghborhoodCentroid;
    [SerializeField] public Vector3 LocalGroupCentroid;
    [SerializeField] public int LocalGroupSize = 3;
    [SerializeField] public SphereCollider NieghborhoodSphere;
    [SerializeField] float _SpeedModifier;


    [SerializeField] float maximumAcceleration = 5;//this isn't really just a speed limmit, it's a rescource. A "portion" of the maximumAcceleration is given out to acceleration requests until there is none left, ensuring there is less indicision in the swarm.
    [SerializeField] public Vector3 Velocity;

    [SerializeField] List<UrgeBehavior> urgeBehaviors;//The list of behaviors that currently contribute acceleration requests.
    List<AccelerationRequest> accelerationRequests = new List<AccelerationRequest>();

    // Start is called before the first frame update
    void Start()
    {
        NieghborhoodSphere.radius = NieghborhoodArea;

        //Get components of type UrgeBehavior
        


        foreach (UrgeBehavior boid in GetComponents<UrgeBehavior>())
        {
            if (!urgeBehaviors.Contains(boid))
            {
                urgeBehaviors.Add(boid);
            }
            boid.Brain = this;
        }

        StartCoroutine(SortNieghborsIntermittent());
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateNieghborhoodCentroid();
        CalculateLocalGroupCentroid();
        Debug.Log(NieghborhoodCentroid);
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
            float requestedAcceleration = (request.Velocity).magnitude;
            if ((totalAcceleration + requestedAcceleration) < maximumAcceleration)//make sure we don't blow our budget for acceleration
            {
                totalAcceleration += requestedAcceleration;
            }
            else
            {
                break;
            }
            if (request.Velocity.magnitude > 0)
            {
                Velocity += request.Velocity;
            }
        }
        
        Velocity = Velocity * Time.deltaTime * _SpeedModifier;
    }

    //The following coroutine sorts the NieghborhoodBoids every 5 seconds
    IEnumerator SortNieghborsIntermittent()
    {
        while (true)
        {
            if (NieghborhoodBoids.Count > 1)
            {
                SortNieghborhoodBoids();
                LocalGroupBoids.Clear();
                for (int i = 0; i < Mathf.Min(LocalGroupSize, NieghborhoodBoids.Count); i++)
                {
                    LocalGroupBoids.Add(NieghborhoodBoids[i]);
                }
            }
            yield return new WaitForSeconds(0f);
        }
    }

    void ProcessAccelerationRequests()
    {
        if (accelerationRequests.Count > 0)
        {
            accelerationRequests.Clear();
        }

        foreach (UrgeBehavior urge in urgeBehaviors)
        {
            urge.SetAccelerationRequest();
            accelerationRequests.Add(urge.CurrentAccelerationRequest);
        }
    }

    void PrioritizeRequests()
    {
        //Sort accelerationRequests by priority
        accelerationRequests.Sort((x, y) => x.Priority.CompareTo(y.Priority));
    }
    

    //calculate the centroid position of NieghborhoodBoids
    public void CalculateNieghborhoodCentroid()
    {
        NieghborhoodCentroid = Vector3.zero;
        foreach (GameObject boid in NieghborhoodBoids)
        {
            NieghborhoodCentroid += boid.transform.position;
        }
        NieghborhoodCentroid /= NieghborhoodBoids.Count;
        
    }

    //Sort the NieghborhoodBoids by distance from the current boid
    public void SortNieghborhoodBoids()
    {
        NieghborhoodBoids.Sort((x, y) => Vector3.Distance(x.transform.position, transform.position).CompareTo(Vector3.Distance(y.transform.position, transform.position)));
    }

    //calculate the centroid position of LocalGroundBoids
    public void CalculateLocalGroupCentroid()
    {
        LocalGroupCentroid = Vector3.zero;
        foreach (GameObject boid in LocalGroupBoids)
        {
            LocalGroupCentroid += boid.transform.position;
        }
        LocalGroupCentroid /= LocalGroupBoids.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            AddNieghborhoodBoid(other.gameObject);
            NieghborhoodCentroid += other.gameObject.transform.position;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            NieghborhoodBoids.Remove(other.gameObject);
            NieghborhoodCentroid -= other.gameObject.transform.position;
        }
    }

    public void AddNieghborhoodBoid(GameObject newBoid)
    {
        if (NieghborhoodBoids.Count < NieghborhoodSizeLimmit)
        {
            NieghborhoodBoids.Add(newBoid);
        }
    }

    


}
