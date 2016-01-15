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
        if (gameController.gameover)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return;
        }

        speed = 20;

        if (Input.GetKey(keyLeft) || TouchLeft())
        {
            //if (currentPos.x - speed - scale > gameController.startX)
            //transform.Translate(-speed, 0, 0);
            rb.AddForce(new Vector2(-speed, 0));
        }

        if (Input.GetKey(keyRight) || TouchRight())
        {
            //if (currentPos.x + speed + scale < gameController.endX)
            //transform.Translate(speed, 0, 0);
            rb.AddForce(new Vector2(speed, 0));
        }
    }
}
