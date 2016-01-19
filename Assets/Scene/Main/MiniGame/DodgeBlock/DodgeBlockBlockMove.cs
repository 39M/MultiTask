using UnityEngine;
using System.Collections;

public class DodgeBlockBlockMove : MonoBehaviour
{
    public float speed;
    public BaseGame baseGame;

    void Update()
    {
        if (baseGame.destroy || transform.position.y < baseGame.startY - 0.2f)
            Destroy(gameObject);

        if (baseGame.gameover)
            return;

        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
            baseGame.gameover = true;
    }
}
