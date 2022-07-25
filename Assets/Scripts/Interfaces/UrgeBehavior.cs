using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UrgeBehavior : MonoBehaviour
{
    public Boid Brain;
    public AccelerationRequest CurrentAccelerationRequest;
    public float strength;
    public float priority;
    public abstract void SetAccelerationRequest();
}
//Creating an urge requires the following steps:
// 1. Create a new class that inherits from UrgeBehavior.
//2. Create an override for the SetAccelerationRequest method
//3. Determine the local properties for the behavior
//4. Produce the logic for the Acceleration request in the corrosponding method that sets
// the CurrentAccelerationRequest.velocity field to the value you wish to have processed
//5. Ensure the requested velocity is normalized and multiplied by strength.
//6. Ensure that the priority and strnegth variables are properly determined.
// Weather that means that they are unchanged after start or need to be calculated is up to you.
// But, generally speaking, you should never have a situation where any behavior could overpower
// object avoidance.