﻿using UnityEngine;
using System.Collections;

public class AvoidArrowHeroController : BaseController
{
    public float speed;
    public BaseGame gameController;
    Rigidbody2D rb;
    //float scale = 0.25f;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Vector3 currentPos = transform.position;
        //speed = 5 * Time.deltaTime;
        speed = 5;

        if (Input.GetKey(keyUp))
        {
            //if (currentPos.y + speed + scale < gameController.endY)
            //transform.Translate(0, speed, 0);
            rb.AddForce(new Vector2(0, speed));
        }

        if (Input.GetKey(keyLeft))
        {
            //if (currentPos.x - speed - scale > gameController.startX)
            //transform.Translate(-speed, 0, 0);
            rb.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(keyDown))
        {
            //if (currentPos.y - speed - scale > gameController.startY)
            //transform.Translate(0, -speed, 0);
            rb.AddForce(new Vector2(0, -speed));
        }

        if (Input.GetKey(keyRight))
        {
            //if (currentPos.x + speed + scale < gameController.endX)
            //transform.Translate(speed, 0, 0);
            rb.AddForce(new Vector2(speed, 0));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with block, game over
        if (other.gameObject.tag == "Block")
            gameController.gameover = true;
    }
}
