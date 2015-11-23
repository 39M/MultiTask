using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour
{
    float force = 5f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move plane up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, force));
        }
    }
}
