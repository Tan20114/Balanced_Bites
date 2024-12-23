using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : CollectableObject
{
    void Start()
    {
        healthinessValues = -999;
        cType = CandyType.Sugar;
        hType = HealthyType.None;
    }
}
