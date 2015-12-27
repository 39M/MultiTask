using UnityEngine;
using System.Collections;

public class Helicopter : BaseGame
{
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject plane;
    public GameObject block;
    float timer = 0;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);

        plane = CreateGameObjectWithRatio(plane, 1 / 5f);
        plane.GetComponent<PlaneController>().isLeft = isLeft;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        // Generate a new block
        if (timer > 2)
        {
            var b = CreateGameObjectWithRatio(block, 0.95f);
            b.transform.Translate(0, Random.Range(startY + 1f, endY - 1f), 0);
            b.GetComponent<BlockMove>().gameController = this;
            timer = Random.Range(-1f, 1f);
        }
    }

    public override void End()
    {
        gameover = true;
        Destroy(floorLimit);
        Destroy(ceilingLimit);
        Destroy(plane);
        Destroy(gameObject);
    }
}
