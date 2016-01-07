using UnityEngine;

public class AvoidArrowHeroController : WASDController
{
    public BaseGame gameController;

    public override void FixedUpdate()
    {
        if (gameController.gameover)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        base.FixedUpdate();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with block, game over
        if (other.gameObject.tag == "Block")
            gameController.gameover = true;
    }
}
