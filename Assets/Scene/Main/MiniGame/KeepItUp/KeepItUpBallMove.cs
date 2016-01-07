using UnityEngine;
using System.Collections;

public class KeepItUpBallMove : MonoBehaviour
{
    public BaseGame gameController;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        Vector2 direction = new Vector2(Random.Range(0.25f, 0.75f), Random.Range(0.3f, 0.7f));
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized / 3f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameover)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            return;
        }

        if (transform.position.y < gameController.startY)
        {
            gameController.gameover = true;
        }
    }
}
