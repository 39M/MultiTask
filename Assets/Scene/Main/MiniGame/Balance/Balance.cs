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
        plantform.GetComponent<PlantformController>().gameController = this;

        ball = CreateGameObject(ball);
        ball.transform.Translate(new Vector3(Random.Range(0.1f, 0.5f) * (Random.value > 0.5 ? 1 : -1), 0));
    }

    public override void Update()
    {
        if (gameover)
        {
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }

        if ((ball.transform.position - plantform.transform.position).magnitude > 2.75f)
            gameover = true;
    }

    public override void End()
    {
        destroy = true;
        Destroy(plantform);
        Destroy(ball);
        Destroy(gameObject);
    }
}
