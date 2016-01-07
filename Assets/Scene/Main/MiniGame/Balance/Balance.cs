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
        plantform.transform.localScale = new Vector3(Mathf.Clamp(600 * Mathf.Pow(0.965f, difficulty), 200f, 600f), 40f, 1f);

        ball = CreateGameObject(ball);
        ball.transform.Translate(new Vector3(Random.Range(0.1f, 0.5f) * (Random.value > 0.5 ? 1 : -1), 0));
        ball.GetComponent<Rigidbody2D>().angularDrag = Mathf.Clamp(10 * Mathf.Pow(0.925f, difficulty), 0.1f, 10f);

    }

    public override void Update()
    {
        if (gameover)
            return;

        // Difficulty setting for debug
        //plantform.transform.localScale = new Vector3(Mathf.Clamp(600 * Mathf.Pow(0.965f, difficulty), 200f, 600f), 40f, 1f);
        //ball.GetComponent<Rigidbody2D>().angularDrag = Mathf.Clamp(10 * Mathf.Pow(0.925f, difficulty), 0.1f, 10f);

        if ((ball.transform.position - plantform.transform.position).magnitude > plantform.transform.localScale.x / 200f + 0.3f)
        {
            gameover = true;
            var rb = ball.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

            rb = plantform.GetComponent<Rigidbody2D>();
            rb.angularVelocity = 0;
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(plantform);
        Destroy(ball);
        Destroy(gameObject);
    }
}
