using UnityEngine;
using System.Collections;

public class PlaneController : MonoBehaviour {
    float force = 5f;
    float posOffset;
    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        posOffset = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(new Vector2(0, force));
        }
    }
}
