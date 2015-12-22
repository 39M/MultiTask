using UnityEngine;
using System.Collections;

public class KeepItUp : BaseGame
{
    public GameObject ball;
    public GameObject guard;
    public GameObject ceilingLimit;
    public GameObject edgeLimit;

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        edgeLimit = CreateVertitalLimit(edgeLimit);

        guard = CreateGameObject(guard);
        guard.GetComponent<GuardController>().isLeft = isLeft;
        ball = CreateGameObject(ball);
        ball.GetComponent<KeepItUpBallMove>().gameController = this;
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
