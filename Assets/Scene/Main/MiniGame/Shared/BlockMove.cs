using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour
{
    public BaseGame gameController;

    void Start()
    {

    }

    void Update()
    {
        // Move left
        transform.Translate(-5 * Time.deltaTime, 0, 0);
        // Destroy when game over or at most left
        if (gameController.gameover || transform.position.x < -4.4 + gameController.offset)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // When collide with hero, game over
        if (other.gameObject.tag == "Hero")
            gameController.gameover = true;
    }
}
