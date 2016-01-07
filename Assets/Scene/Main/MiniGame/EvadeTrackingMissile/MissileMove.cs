using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    public BaseGame gameController;
    public Transform heroTransform;
    public float flexibility;
    public float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (gameController.gameover)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        Vector2 direction = heroTransform.position - transform.position;
        rb.velocity = Vector2.MoveTowards(rb.velocity, direction.normalized * speed, flexibility * 2f);
    }

    void Update()
    {
        if (gameController.destroy)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hero"))
            gameController.gameover = true;
        if (other.gameObject.CompareTag("Limit"))
            Destroy(gameObject);
    }
}
