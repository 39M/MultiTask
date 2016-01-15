using UnityEngine;
using System.Collections;

public class PlaneController : BaseController
{
    public int controlMethod;
    float force = 0.4f;
    Rigidbody2D rb;
    bool reverseKeyDown = false;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        if (controlMethod == 2)
            rb.gravityScale *= -3f;
    }

    void Update()
    {
        if (Input.GetKeyDown(keyUp) || Input.GetKeyDown(keyDown) || TouchAnyWhere(TouchPhase.Began))
            reverseKeyDown = true;
    }

    void FixedUpdate()
    {
        if (baseGame.gameover)
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
        if (Input.GetKey(keyUp) || TouchInAreaY(1 / 5f, 1f))
        {
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }

        // Move plane down
        if (Input.GetKey(keyDown) || TouchInAreaY(0, 1 / 5f))
        {
            rb.AddForce(new Vector2(0, -force), ForceMode2D.Impulse);
        }
    }

    void FlappyController()
    {
        if (reverseKeyDown)
        {
            //rb.AddForce(new Vector2(0, force * 20), ForceMode2D.Impulse);
            rb.velocity = new Vector2(0, 6f);
            reverseKeyDown = false;
        }
    }

    void AntiGravityController()
    {
        if (reverseKeyDown)
        {
            rb.gravityScale = -rb.gravityScale;
            reverseKeyDown = false;
        }
    }
}
