using UnityEngine;
using System.Collections;

public class PlaneController : BaseController
{
    float force = 0.4f;
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

        // Move plane up
        if (Input.GetKey(keyUp))
        {
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }
    }
}
