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
        if (baseGame.gameover)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return;
        }

        speed = 20;

        var dir = TouchPointVector();
        if (dir != Vector3.zero)
        {
            dir.x = Mathf.Clamp(dir.x, -2, 2) * speed / 2f;
            dir.y = 0;
            rb.AddForce(dir);
        }

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
