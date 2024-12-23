using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : CollectableObject
{
    private void Start()
    {
        healthinessValues = -1;
        cType = CandyType.Chocolate;
        hType = HealthyType.None;
    }
}
