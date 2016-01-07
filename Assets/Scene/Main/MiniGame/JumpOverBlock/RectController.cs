using UnityEngine;
using System.Collections;

public class RectController : BaseController
{
    float force = 30f;
    Rigidbody2D rb;
    public bool onLand = true;

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

        if (Input.GetKey(keyUp) && onLand)
        {
            rb.AddForce(new Vector2(0, force));
            onLand = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Land")
            onLand = true;
    }
}
