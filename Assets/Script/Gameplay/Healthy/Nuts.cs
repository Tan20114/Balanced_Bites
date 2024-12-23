using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuts : CollectableObject
{
    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();

        healthinessValues = 1;
        cType = CandyType.None;
        hType = HealthyType.Nuts;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.isShield = true;
            Debug.Log("Nuts");
        }
        base.OnCollisionEnter2D(collision);
    }
}
