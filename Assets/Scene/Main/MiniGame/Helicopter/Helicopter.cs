using UnityEngine;
using System.Collections;

public class Helicopter : BaseGame
{
    public GameObject limit;
    public GameObject plane;
    public GameObject block;
    float timer = 0;

    public override void Start()
    {
        base.Start();

        limit = CreateGameObject(limit);

        plane = CreateGameObject(plane);
        plane.GetComponent<PlaneController>().isLeft = isLeft;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        // Generate a new block
        if (timer > 1)
        {
            var b = Instantiate(block);
            b.transform.Translate(offset, Random.Range(-4f, 4f), 0);
            b.GetComponent<BlockMove>().gameController = this;
            timer = 0;
        }
    }

    public override void End()
    {
        gameover = true;
        Destroy(limit);
        Destroy(plane);
        Destroy(gameObject);
    }
}
