using UnityEngine;
using System.Collections;

public class KeepItUp : BaseGame
{
    public GameObject ball;
    public GameObject guard;
    public GameObject ceilingLimit;
    public GameObject leftLimit;
    public GameObject rightLimit;
    GameObject edgeLimit;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateGameObject(ceilingLimit);
        if (isLeft)
            edgeLimit = Instantiate(leftLimit);
        else
            edgeLimit = Instantiate(rightLimit);

        guard = CreateGameObject(guard);
        ball = CreateGameObject(ball);
        ball.GetComponent<KeepItUpBallMove>().gameController = this;

    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void End()
    {
        gameover = true;
        Destroy(ceilingLimit);
        Destroy(edgeLimit);
        Destroy(ball);
        Destroy(guard);
        Destroy(gameObject);
    }
}
