using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : CollectableObject
{
    Rigidbody2D rb;

    [Range(0f, 1f)]
    [SerializeField] float delay;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        healthinessValues = 5;
        cType = CandyType.None;
        hType = HealthyType.Lemon;
    }

    void Update()
    {
        rb.velocity = new Vector3(-GameManager.Instance.player.rb.velocity.x * delay, 0, 0);
    }
}
