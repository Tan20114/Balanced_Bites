using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youghurt : CollectableObject
{
    void Start()
    {
        healthinessValues = 2;
        cType = CandyType.None;
        hType = HealthyType.Youghurt;
    }
}
