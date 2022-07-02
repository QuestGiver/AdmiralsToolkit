using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class UrgeBehavior : MonoBehaviour
{
    public Boid Brain;
    public AccelerationRequest CurrentAccelerationRequest;
    public float strength;
    public abstract void GenerateAccelerationRequest();
}
