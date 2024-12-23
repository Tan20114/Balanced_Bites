using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : CollectableObject
{
    private void Start()
    {
        healthinessValues = 1;
        cType = CandyType.None;
        hType = HealthyType.Berry;
    }

    override public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayerMoveSpeed += 1;
        }
        base.OnCollisionEnter2D(collision);
    }
}
