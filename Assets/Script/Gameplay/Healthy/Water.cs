using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : CollectableObject
{
    void Start()
    {
        healthinessValues = 999;
        cType = CandyType.None;
        hType = HealthyType.Water;
    }
}
