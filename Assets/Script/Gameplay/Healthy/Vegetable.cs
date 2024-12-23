using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vegetable : CollectableObject
{
    private void Start()
    {
        healthinessValues = 1;
        cType = CandyType.None;
        hType = HealthyType.Vegetable;
    }
}
