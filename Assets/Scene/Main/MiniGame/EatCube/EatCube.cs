using UnityEngine;
using System.Collections;

public class EatCube : BaseGame
{
    public GameObject hero;
    public GameObject food;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject edgeLimit;

    float foodGenTime;
    float foodGenMinTime;
    float foodGenTimeRandomRange;
    float foodLifeTime;

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

    public void SetDifficulty()
    {
        foodGenTime = 4f * Mathf.Pow(0.95f, difficulty);
        foodGenMinTime = -0.25f;
        foodGenTimeRandomRange = 4f * Mathf.Pow(0.85f, difficulty);
        foodLifeTime = Mathf.Clamp(15f * Mathf.Pow(0.95f, difficulty), 7.5f, 15f);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        SetDifficulty();

        if (timer < foodGenTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(foodGenMinTime - foodGenTimeRandomRange, foodGenMinTime);
            Vector3 foodPos;
            do
            {
                foodPos = new Vector3(Random.Range(startX + foodScalehalf, endX - foodScalehalf),
                                Random.Range(startY + foodScalehalf, endY - foodScalehalf));
            } while ((foodPos - hero.transform.position).magnitude < 1f);

            FoodController fc = ((GameObject)Instantiate(food, foodPos, food.transform.rotation)).GetComponent<FoodController>();
            fc.gameController = this;
            fc.totalTime = foodLifeTime;
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
