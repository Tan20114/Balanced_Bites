using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCandy : CollectableObject
{
    void Start()
    {
        healthinessValues = -2;
        cType = CandyType.HardCandy;
        hType = HealthyType.None;
    }
}