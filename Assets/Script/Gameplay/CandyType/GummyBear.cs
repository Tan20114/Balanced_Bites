using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GummyBear : CollectableObject
{
    private Rigidbody2D prb;

    [Range(0,10)]
    [SerializeField] float forceMagnitude;

    private void Start()
    {
        healthinessValues = -1;
        cType = CandyType.GummyBear;
        hType = HealthyType.None;
    }

    override public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            prb = collision.gameObject.GetComponent<Rigidbody2D>();

            if(!prb.GetComponent<Player>().isShield)
            {
                prb.AddForce(new(forceMagnitude * RandomForceDir(), 0), ForceMode2D.Impulse);
                prb.GetComponent<Player>().isBounce = true;
            }
        }
        base.OnCollisionEnter2D(collision);
    }

    int RandomForceDir()
    {
        int forceDir = Random.Range(0, 2);

        forceDir = (forceDir == 0)? -1 : forceDir;

        return forceDir;
    }
}
