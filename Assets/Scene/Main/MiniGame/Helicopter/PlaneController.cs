using UnityEngine;
using System.Collections;

public class PlaneController : BaseController
{
    float force = 2.5f;
    Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move plane up
        if (Input.GetKey(keyUp))
        {
            rb.AddForce(new Vector2(0, force));
        }
    }
}
