using UnityEngine;
using System.Collections;

public class AvoidArrow : BaseGame
{
    public GameObject hero;
    public GameObject arrow;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject leftLimit;
    public GameObject rightLimit;
    GameObject edgeLimit;

    float timer = 0;


    // Use this for initialization
    public override void Start()
    {
        base.Start();

        ceilingLimit = CreateGameObject(ceilingLimit);
        floorLimit = CreateGameObject(floorLimit);
        if (isLeft)
            edgeLimit = Instantiate(leftLimit);
        else
            edgeLimit = Instantiate(rightLimit);

        hero = CreateGameObject(hero);
        var heroController = hero.GetComponent<AvoidArrowHeroController>();
        heroController.gameController = this;
        heroController.isLeft = isLeft;
    }

    public override void Update()
    {
        if (gameover)
            return;

        if (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;

            Vector3 arrowPos;
            switch (Random.Range(0, 4))
            {
                case 0:
                    arrowPos = new Vector3(Random.Range(startX, endX), startY);
                    break;
                case 1:
                    arrowPos = new Vector3(Random.Range(startX, endX), endY);
                    break;
                case 2:
                    arrowPos = new Vector3(startX, Random.Range(startY, endY));
                    break;
                default:
                    arrowPos = new Vector3(endX, Random.Range(startY, endY));
                    break;
            }

            ArrowMove arrowMoveScript = ((GameObject)Instantiate(arrow, arrowPos, arrow.transform.rotation)).GetComponent<ArrowMove>();
            arrowMoveScript.hero = hero;
            arrowMoveScript.gameController = this;
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
