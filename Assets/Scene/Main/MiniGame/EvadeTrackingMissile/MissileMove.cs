using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    public BaseGame gameController;
    public Transform heroTransform;
    public float flexibility;
    public float speed;
	Rigidbody2D rb;
	ParticleSystem ps;
	bool hitLimit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		ps = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (gameController.gameover)
        {
            rb.velocity = Vector3.zero;
            ps.Pause();
            return;
        }

		if (hitLimit)
		{
			ps.enableEmission = false;
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<Rigidbody2D>().Sleep();
			Destroy(gameObject, 0.5f);
			return;
		}

        // Follow hero
        Vector2 direction = heroTransform.position - transform.position;
        rb.velocity = Vector2.MoveTowards(rb.velocity, direction.normalized * speed, flexibility * 2f);

        // Rotate itself
        transform.rotation = Quaternion.identity;
        int towards = direction.y < 0 ? -1 : 1;
        transform.Rotate(new Vector3(0, 0, 90 + towards * Vector3.Angle(direction, new Vector3(1, 0, 0))));
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
			hitLimit = true;
    }
}
