using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour
{
    public BaseGame gameController;
    public float moveSpeed = 3;
    SpriteRenderer render;
    Color blockColor;
    float fadeTime = 0.3f;
    float deadPos;
    bool fullyShow = false;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        blockColor = render.color;
        blockColor.a = 0;
        render.color = blockColor;
        blockColor.a = 1;

        deadPos = gameController.offset - Mathf.Abs(gameController.offset) * 0.9f;
    }

    void Update()
    {
        // Destroy when game over or at most left
        if (gameController.destroy || transform.position.x < deadPos)
            Destroy(gameObject);

        if (gameController.gameover)
            return;

        // Move left
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);

        // Fade out
        if (transform.position.x < deadPos + fadeTime * moveSpeed)
        {
            render.color = Color.Lerp(render.color, Color.clear, Time.deltaTime * 10f);
        }

        // Fade in
        if (!fullyShow)
        {
            render.color = Color.Lerp(render.color, blockColor, Time.deltaTime * 10f);
            if (render.color.a > 0.9999f)
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
