using UnityEngine;
using System.Collections;

public class PlaneController : BaseController
{
    public int controlMethod;
    float force = 0.4f;
    Rigidbody2D rb;
    bool upKeyDown = false;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyUp))
            upKeyDown = true;
    }

    void FixedUpdate()
    {
        if (gameController.gameover)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return;
        }

        switch (controlMethod)
        {
            case 1:
                FlappyController();
                break;
            case 2:
                AntiGravityController();
                break;
            default:
                ClassicHelicopterController();
                break;
        }
    }

    void ClassicHelicopterController()
    {
        // Move plane up
        if (Input.GetKey(keyUp))
        {
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }

        // Move plane down
        if (Input.GetKey(keyDown))
        {
            rb.AddForce(new Vector2(0, -force / 2f), ForceMode2D.Impulse);
        }
    }

    void FlappyController()
    {
        if (upKeyDown)
        {
            //rb.AddForce(new Vector2(0, force * 20), ForceMode2D.Impulse);
            rb.velocity = new Vector2(0, 6f);
            upKeyDown = false;
        }
    }

    void AntiGravityController()
    {
        if (upKeyDown)
        {
            rb.gravityScale = -rb.gravityScale;
            upKeyDown = false;
        }
    }
}
