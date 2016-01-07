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
        guard.GetComponent<GuardController>().gameController = this;
        //guard.transform.localScale = new Vector3((endX - startX) * 20, 25, 1);

        ball = CreateGameObject(ball);
        ball.GetComponent<KeepItUpBallMove>().gameController = this;
    }

    public override void Update()
    {
        guard.transform.localScale = new Vector3(Mathf.Clamp(400 * Mathf.Pow(0.955f, difficulty), 100, 400), 25, 1);
    }

    public override void End()
    {
        destroy = true;
        Destroy(ceilingLimit);
        Destroy(edgeLimit);
        Destroy(ball);
        Destroy(guard);
        Destroy(gameObject);
    }
}
