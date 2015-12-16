using UnityEngine;
using System.Collections;

public class KeepItUpBallMove : MonoBehaviour
{
    public BaseGame gameController;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0.5f), ForceMode2D.Impulse);
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
