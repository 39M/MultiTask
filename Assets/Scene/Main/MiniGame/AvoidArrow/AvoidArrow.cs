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

        ceilingLimit = Instantiate(ceilingLimit);
        ceilingLimit.transform.Translate(offset, 0, 0);
        floorLimit = Instantiate(floorLimit);
        floorLimit.transform.Translate(offset, 0, 0);
        if (isLeft)
            edgeLimit = Instantiate(leftLimit);
        else
            edgeLimit = Instantiate(rightLimit);

        hero = Instantiate(hero);
        hero.transform.Translate(offset, 0, 0);
        hero.GetComponent<AvoidArrowHeroController>().gameController = this;
    }

    public override void Update()
    {
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
