using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour
{
    public BaseGame gameController;
    SpriteRenderer render;
    Color blockColor;
    float moveSpeed = 3;
    float fadeTime = 0.3f;
    float deadPos;
    bool fullyShow = false;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        blockColor = render.color;
        blockColor.a = 0;
        render.color = blockColor;
        deadPos = gameController.offset - Mathf.Abs(gameController.offset) * 0.9f;
    }

    void Update()
    {
        // Move left
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        // Destroy when game over or at most left
        if (gameController.gameover || transform.position.x < deadPos)
            Destroy(gameObject);

        // Fade out
        if (transform.position.x < deadPos + fadeTime * moveSpeed)
        {
            blockColor.a -= 1.0f / fadeTime * Time.deltaTime;
            render.color = blockColor;
        }

        // Fade in
        if (!fullyShow)
        {
            blockColor.a += 1.0f / fadeTime * Time.deltaTime;
            render.color = blockColor;
            if (blockColor.a >= 1)
                fullyShow = true;
        }
        else
        {
            fullyShow = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // When collide with hero, game over
        if (other.gameObject.tag == "Hero")
            gameController.gameover = true;
    }
}
