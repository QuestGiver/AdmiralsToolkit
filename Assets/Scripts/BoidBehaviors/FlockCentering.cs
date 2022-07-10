using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockCentering : UrgeBehavior
{
    void Start()
    {
        CurrentAccelerationRequest = new AccelerationRequest();
    }

    public override void SetAccelerationRequest()
    {

    }
}
