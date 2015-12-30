﻿using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    public BaseGame gameController;
    public Transform heroTransform;
    public float flexibility;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction = heroTransform.position - transform.position;
        rb.velocity = Vector2.MoveTowards(rb.velocity, direction.normalized * 2f, flexibility * 2f);
    }

    void Update()
    {
        if (gameController.gameover)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hero"))
            gameController.gameover = true;
        Destroy(gameObject);
    }
}
