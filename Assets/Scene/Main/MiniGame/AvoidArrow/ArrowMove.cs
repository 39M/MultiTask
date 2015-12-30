using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour
{
    public GameObject hero;
    public BaseGame gameController;
    Vector3 direction;


    void Start()
    {
        direction = hero.transform.position - transform.position;
        direction = direction.normalized * 1.5f;
    }


    void Update()
    {
        transform.Translate(direction * Time.deltaTime);

        float posX = transform.position.x;
        float posY = transform.position.y;

        if (gameController.gameover)
            Destroy(gameObject);

        if (posX < gameController.startX || posX > gameController.endX)
            Destroy(gameObject);

        if (posY < gameController.startY || posY > gameController.endY)
            Destroy(gameObject);
    }
}
