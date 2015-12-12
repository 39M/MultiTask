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

        ball = Instantiate(ball);
        ball.transform.Translate(offset, 0.1f, 0);
    }

    public override void Update()
    {
        if (ball.transform.position.y < -5)
            gameover = true;
    }

    public override void End()
    {
        Destroy(plantform);
        Destroy(ball);
        Destroy(gameObject);
    }
}
