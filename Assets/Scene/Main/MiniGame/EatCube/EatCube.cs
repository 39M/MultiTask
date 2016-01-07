using UnityEngine;
using System.Collections;

public class EatCube : BaseGame
{
    public GameObject hero;
    public GameObject food;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject edgeLimit;

    float foodScalehalf = 0.5f;
    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);
        edgeLimit = CreateVertitalLimit(edgeLimit);

        hero = CreateGameObject(hero);
        hero.GetComponent<WASDController>().isLeft = isLeft;
        hero.GetComponent<WASDController>().gameController = this;
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        if (timer < 4f * Mathf.Pow(0.935f, difficulty))
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(-0.1f - 4f * Mathf.Pow(0.825f, difficulty), -0.1f);
            Vector3 foodPos;
            do
            {
                foodPos = new Vector3(Random.Range(startX + foodScalehalf, endX - foodScalehalf),
                                Random.Range(startY + foodScalehalf, endY - foodScalehalf));
            } while ((foodPos - hero.transform.position).magnitude < 1f);

            FoodController fc = ((GameObject)Instantiate(food, foodPos, food.transform.rotation)).GetComponent<FoodController>();
            fc.gameController = this;
            fc.totalTime = Mathf.Clamp(15f * Mathf.Pow(0.95f, difficulty), 7f, 15f);
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(floorLimit);
        Destroy(ceilingLimit);
        Destroy(edgeLimit);
        Destroy(hero);
        Destroy(gameObject);
    }
}
