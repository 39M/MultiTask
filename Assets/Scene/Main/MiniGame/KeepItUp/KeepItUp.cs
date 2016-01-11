using UnityEngine;
using System.Collections;

public class KeepItUp : BaseGame
{
    public GameObject ball;
    public GameObject guard;
    public GameObject ceilingLimit;
    public GameObject edgeLimit;

    float guardLength;
    float ballSpeed;

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

        SetDifficulty();

        ball = CreateGameObject(ball);
        ball.GetComponent<KeepItUpBallMove>().gameController = this;
        ball.GetComponent<KeepItUpBallMove>().speed = ballSpeed;
    }

    public void SetDifficulty()
    {
        guardLength = Mathf.Clamp(400 * Mathf.Pow(0.975f, difficulty), 100, 400);
        ballSpeed = Mathf.Clamp(Mathf.Pow(1.03f, difficulty) / 3f, 0, 1.5f);
    }

    public override void Update()
    {
        SetDifficulty();

        guard.transform.localScale = new Vector3(guardLength, 25, 1);        
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
