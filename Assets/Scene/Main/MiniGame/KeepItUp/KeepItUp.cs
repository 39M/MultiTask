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
        guard.transform.localScale = new Vector3((endX - startX) * 20, 25, 1);

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
