using UnityEngine;
using System.Collections;

public class GuardController : BaseController
{
    public float speed;
    Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        speed = 15;

        if (Input.GetKey(keyLeft))
        {
            //if (currentPos.x - speed - scale > gameController.startX)
            //transform.Translate(-speed, 0, 0);
            rb.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(keyRight))
        {
            //if (currentPos.x + speed + scale < gameController.endX)
            //transform.Translate(speed, 0, 0);
            rb.AddForce(new Vector2(speed, 0));
        }
    }
}
