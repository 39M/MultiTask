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
    float timer = 2.5f;

    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);
        edgeLimit = CreateVertitalLimit(edgeLimit);

        hero = CreateGameObject(hero);
        hero.GetComponent<EatCubeHeroController>().isLeft = isLeft;
    }

    public override void Update()
    {
        if (gameover)
            return;

        if (timer < 2.5f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            Vector3 foodPos = new Vector3(Random.Range(startX + foodScalehalf, endX - foodScalehalf),
                Random.Range(startY + foodScalehalf, endY - foodScalehalf));
            FoodController fc = ((GameObject)Instantiate(food, foodPos, food.transform.rotation)).GetComponent<FoodController>();
            fc.gameController = this;
        }
    }

    public override void End()
    {
        gameover = true;
        Destroy(floorLimit);
        Destroy(ceilingLimit);
        Destroy(edgeLimit);
        Destroy(hero);
        Destroy(gameObject);
    }
}
