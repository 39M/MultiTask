using UnityEngine;
using System.Collections;

public class Helicopter : BaseGame
{
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject plane;
    public GameObject block;
    public int controlMethod;
    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);

        plane = CreateGameObjectWithRatio(plane, 1 / 5f);
        plane.GetComponent<PlaneController>().isLeft = isLeft;
        plane.GetComponent<PlaneController>().gameController = this;
        controlMethod = Random.Range(0, 3);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        timer += Time.deltaTime;
        plane.GetComponent<PlaneController>().controlMethod = controlMethod;

        // Generate a new block
        if (timer > 3f * Mathf.Pow(0.92f, difficulty))
        {
            if (Random.value > Mathf.Pow(0.95f, difficulty))
            {
                GenerateBlock(1);
                GenerateBlock(2);
            }
            else
            {
                GenerateBlock(Random.Range(0, 3));
            }
        }
    }

    public void GenerateBlock(int blockPos)
    {
        var b = CreateGameObjectWithRatio(block, 0.95f);
        float scaleY = Mathf.Clamp(Random.Range(150, 200) * Mathf.Pow(1.02f, difficulty), 100, 350);
        b.transform.localScale = new Vector3(25, scaleY, 1);

        if (blockPos == 1)
            b.transform.Translate(0, startY + scaleY / 200f, 0);
        if (blockPos == 2)
            b.transform.Translate(0, endY - scaleY / 200f, 0);

        b.GetComponent<BlockMove>().gameController = this;
        b.GetComponent<BlockMove>().moveSpeed = Mathf.Clamp(2 * Mathf.Pow(1.04f, difficulty), 0, 5f);
        timer = Random.Range(-0.75f - 2f * Mathf.Pow(0.92f, difficulty), -0.75f);
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
