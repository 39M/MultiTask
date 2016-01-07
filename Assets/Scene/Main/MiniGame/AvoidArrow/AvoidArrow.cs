using UnityEngine;
using System.Collections;

public class AvoidArrow : BaseGame
{
    public GameObject hero;
    public GameObject arrow;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject edgeLimit;
    float blockGenRate;

    float timer = float.MaxValue;


    // Use this for initialization
    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateHorizontalLimit(ceilingLimit, 1);
        floorLimit = CreateHorizontalLimit(floorLimit, -1);
        edgeLimit = CreateVertitalLimit(edgeLimit);

        hero = CreateGameObject(hero);
        var heroController = hero.GetComponent<AvoidArrowHeroController>();
        heroController.gameController = this;
        heroController.isLeft = isLeft;

        blockGenRate = 4f * Mathf.Pow(0.85f, difficulty);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        // Difficulty setting for debug
        //blockGenRate = 4f * Mathf.Pow(0.85f, difficulty);
        if (timer < blockGenRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(-0.1f - Mathf.Pow(0.85f, difficulty), -0.1f);

            Vector3 arrowPos;
            do
            {
                arrowPos = randEdgePosition();
            } while ((arrowPos - hero.transform.position).magnitude < 1f);


            ArrowMove arrowMoveScript = ((GameObject)Instantiate(arrow, arrowPos, arrow.transform.rotation)).GetComponent<ArrowMove>();
            arrowMoveScript.hero = hero;
            arrowMoveScript.gameController = this;
            arrowMoveScript.speed = Mathf.Clamp(Mathf.Pow(1.05f, difficulty), 1f, 10f);
        }
    }

    Vector3 randEdgePosition()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                return new Vector3(Random.Range(startX, endX), startY);
            case 1:
                return new Vector3(Random.Range(startX, endX), endY);
            case 2:
                return new Vector3(startX, Random.Range(startY, endY));
            default:
                return new Vector3(endX, Random.Range(startY, endY));
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
