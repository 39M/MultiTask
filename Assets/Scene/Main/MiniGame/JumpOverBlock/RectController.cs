using UnityEngine;
using System.Collections;

public class RectController : BaseController
{
    float force = 35f;
    Rigidbody2D rb;
    public bool onLand = true;
    bool secondTouchEnable = false;
    bool touchDown = false;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (TouchAnyWhere(TouchPhase.Began))
            touchDown = true;
    }

    void FixedUpdate()
    {
        if (gameController.gameover)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return;
        }

        if (onLand)
        {
            if ((Input.GetKey(keyUp) || touchDown))
            {
                rb.AddForce(new Vector2(0, force));
                onLand = false;
                touchDown = false;
            }
        }
        else
        {
            if (touchDown)
            {
                secondTouchEnable = true;
                touchDown = false;
            }

            if ((Input.GetKey(keyDown) || (secondTouchEnable && TouchAnyWhere())))
            {
                rb.AddForce(new Vector2(0, -force / 2f));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Land")
        {
            onLand = true;
            secondTouchEnable = false;
        }
    }
}
