using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Lollipop : CollectableObject
{
    Transform pivot;

    [SerializeField] int rotationSpeed;

    private void Start()
    {     
        pivot = CandyManager.Instance.lollipopPivot;
        HandSet(RandomHandLength());
        healthinessValues = -5;
        cType = CandyType.Lollipop;
        hType = HealthyType.None;
    }

    private void Update()
    {
        if (pivot.gameObject.CompareTag("LeftPivot"))
            pivot.transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
        else if (pivot.gameObject.CompareTag("RightPivot"))
            pivot.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    int RandomHandLength()
    {
        return Random.Range(4, 8);
    }

    void HandSet(int handLength)
    {
        float xValue = transform.position.x;

        gameObject.transform.position = new Vector3(xValue, handLength, 0);
    }
}