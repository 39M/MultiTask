using UnityEngine;

public class AvoidArrowHeroController : WASDController
{
    public BaseGame gameController;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with block, game over
        if (other.gameObject.tag == "Block")
            gameController.gameover = true;
    }
}
