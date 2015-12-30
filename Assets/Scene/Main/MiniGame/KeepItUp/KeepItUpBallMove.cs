using UnityEngine;
using System.Collections;

public class KeepItUpBallMove : MonoBehaviour
{
    public BaseGame gameController;

    // Use this for initialization
    void Start()
    {
        Vector2 direction = new Vector2(Random.Range(0.25f, 0.75f), Random.Range(0.3f, 0.7f));
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction.normalized / 3f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < gameController.startY)
        {
            gameController.gameover = true;
            //Destroy(gameObject);
        }
    }
}
