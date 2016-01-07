using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour
{
    public GameObject hero;
    public BaseGame gameController;
    public float speed = 1;
    Vector3 direction;


    void Start()
    {
        // Move towards hero
        direction = hero.transform.position - transform.position;
        // Random rotate between (-15, 15)
        direction = (Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * direction).normalized;
    }


    void Update()
    {
        if (gameController.destroy)
            Destroy(gameObject);

        if (gameController.gameover)
            return;
        
        transform.Translate(speed * direction * Time.deltaTime);

        float posX = transform.position.x;
        float posY = transform.position.y;        
        if (posX < gameController.startX || posX > gameController.endX)
            Destroy(gameObject);
        if (posY < gameController.startY || posY > gameController.endY)
            Destroy(gameObject);
    }
}
