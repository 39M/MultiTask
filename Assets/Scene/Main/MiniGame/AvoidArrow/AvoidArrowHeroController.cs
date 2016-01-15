using UnityEngine;

public class AvoidArrowHeroController : WASDController
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with block, game over
        if (other.gameObject.tag == "Block")
            baseGame.gameover = true;
    }
}
