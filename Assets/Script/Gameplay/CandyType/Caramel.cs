using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caramel : CollectableObject
{
    Player player;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();

        healthinessValues = -1;
        cType = CandyType.Caramel;
        hType = HealthyType.None;
    }

    override public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(!player.isShield)
                GameManager.Instance.PlayerMoveSpeed -= 1;
        }
        base.OnCollisionEnter2D(collision);
    }
}
