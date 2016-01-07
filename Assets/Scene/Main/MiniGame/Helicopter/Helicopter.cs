using UnityEngine;
using System.Collections;

public class Helicopter : BaseGame
{
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject plane;
    public GameObject block;
    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);

        plane = CreateGameObjectWithRatio(plane, 1 / 5f);
        plane.GetComponent<PlaneController>().isLeft = isLeft;
        plane.GetComponent<PlaneController>().gameController = this;
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        timer += Time.deltaTime;

        // Generate a new block
        if (timer > 3f * Mathf.Pow(0.92f, difficulty))
        {
            var b = CreateGameObjectWithRatio(block, 0.95f);
            float scaleY = Mathf.Clamp(Random.Range(150, 200) * Mathf.Pow(1.02f, difficulty), 100, 300);
            b.transform.localScale = new Vector3(25, scaleY, 1);
            b.transform.Translate(0, Random.Range(startY + scaleY / 200f, endY - scaleY / 200f), 0);
            b.GetComponent<BlockMove>().gameController = this;
            b.GetComponent<BlockMove>().moveSpeed = Mathf.Clamp(2 * Mathf.Pow(1.04f, difficulty), 0, 5f);
            timer = Random.Range(0 - 2f * Mathf.Pow(0.92f, difficulty), 0);
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(floorLimit);
        Destroy(ceilingLimit);
        Destroy(plane);
        Destroy(gameObject);
    }
}
