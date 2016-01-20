using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour
{
    public GameObject hero;
    public BaseGame gameController;
    public float speed = 1;
    Vector3 direction;
	ParticleSystem ps;
	bool hitLimit = false;

    void Start()
    {
        // Move towards hero
        direction = hero.transform.position - transform.position;
        // Random rotate between (-15, 15)
        direction = (Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction).normalized;

        // Rotate it self
        int towards = direction.y < 0 ? -1 : 1;
        transform.Rotate(new Vector3(0, 0, 90 + towards * Vector3.Angle(direction, new Vector3(1, 0, 0))));

		ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (gameController.destroy)
            Destroy(gameObject);

		if (hitLimit)
		{
			ps.enableEmission = false;
			GetComponent<SpriteRenderer>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<Rigidbody2D>().Sleep();
			Destroy(gameObject, 0.5f);
			return;
		}

        if (gameController.gameover)
        {
            ps.Pause();
            return;
        }

        transform.Translate(speed * direction * Time.deltaTime, Space.World);

        float posX = transform.position.x;
        float posY = transform.position.y;
        if (posX < gameController.startX || posX > gameController.endX)
			hitLimit = true;
        if (posY < gameController.startY || posY > gameController.endY)
			hitLimit = true;
    }
}
