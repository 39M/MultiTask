using UnityEngine;
using System.Collections;

public class CubeMove : MonoBehaviour
{
    public float speed;
    public BaseGame baseGame;
    bool eaten = false;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (baseGame.destroy)
            Destroy(gameObject);

        if (baseGame.gameover)
            return;

        if (eaten)
        {
            transform.localScale *= 1 + Time.deltaTime * 5f;
            sr.color = Color.Lerp(sr.color, Color.clear, Time.deltaTime * 10f);
            if (sr.color.a < 1e-2)
                Destroy(gameObject);
            return;
        }

        transform.Translate(0, -speed * Time.deltaTime, 0);
        if (transform.position.y < baseGame.startY - 0.2f)
            baseGame.gameover = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
            eaten = true;
    }
}
