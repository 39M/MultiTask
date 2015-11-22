using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
    float force = 5f;
    float posOffset;
    Rigidbody2D rb;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        posOffset = transform.position.x;
    }
	
	void Update () {
        // Move plane up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, force));
        }
    }
}
