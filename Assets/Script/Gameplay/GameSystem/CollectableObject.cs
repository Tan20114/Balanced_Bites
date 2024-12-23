using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableObject : MonoBehaviour
{
    public CandyType cType;
    public HealthyType hType;

    public int healthinessValues;

    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}