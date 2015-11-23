using UnityEngine;
using System.Collections;

public class PlantformController : MonoBehaviour
{
    float rotateStep = -5f;
    float posOffset;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posOffset = GetComponent<HingeJoint2D>().connectedAnchor.x;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForceAtPosition(new Vector2(0, rotateStep), new Vector2(-1 + posOffset, 0));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForceAtPosition(new Vector2(0, rotateStep), new Vector2(1 + posOffset, 0));
        }
    }
}
