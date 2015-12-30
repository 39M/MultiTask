using UnityEngine;
using System.Collections;

public class Balance : BaseGame
{
    public GameObject plantform;
    public GameObject ball;

    public override void Start()
    {
        base.Start();

        plantform = (GameObject)Instantiate(plantform, new Vector3(offset, 0), plantform.transform.rotation);
        plantform.GetComponent<HingeJoint2D>().connectedAnchor += new Vector2(offset, 0);
        plantform.GetComponent<PlantformController>().isLeft = isLeft;

        ball = CreateGameObject(ball);
        ball.transform.Translate(new Vector3(Random.Range(0.1f, 0.5f) * (Random.value > 0.5 ? 1 : -1), 0));
    }

    public override void Update()
    {
        if (ball.transform.position.y < startY)
            gameover = true;
    }

    public override void End()
    {
        Destroy(plantform);
        Destroy(ball);
        Destroy(gameObject);
    }
}
