using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour {
    public Helicopter gameController;

	void Start () {
	
	}
	
	void Update () {
        // Move left
        transform.Translate(-5 * Time.deltaTime, 0, 0);
        // Destroy when game over or at most left
        if (gameController.gameover || transform.position.x < -4.4 + gameController.offset)
            Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        // When collide with plane, game over
        if (other.gameObject.tag == "Plane")
            gameController.gameover = true;
    }
}
