using UnityEngine;
using System.Collections;

public class PlantformController : BaseController
{
    float rotateStep = -10f;
    float posOffset;
    Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        posOffset = GetComponent<HingeJoint2D>().connectedAnchor.x;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(keyLeft))
        {
            rb.AddForceAtPosition(new Vector2(0, rotateStep), new Vector2(-1 + posOffset, 0));
        }

        if (Input.GetKey(keyRight))
        {
            rb.AddForceAtPosition(new Vector2(0, rotateStep), new Vector2(1 + posOffset, 0));
        }
    }
}
