using UnityEngine;
using System.Collections;

public class AvoidArrow : BaseGame
{
    public GameObject hero;
    public GameObject arrow;
    public GameObject ceilingLimit;
    public GameObject floorLimit;
    public GameObject edgeLimit;
    float arrowGenTime;
    float arrowMinGenTime;
    float arrowGenTimeRandomRange;
    float arrowSpeed;

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
    }

    public void SetDifficulty()
    {
        // Generate time between two block
        arrowGenTime = 4f * Mathf.Pow(0.9f, difficulty);
        // Min generate time
        arrowMinGenTime = -0.15f;
        // Random range
        arrowGenTimeRandomRange = Mathf.Pow(0.9f, difficulty);
        // Arrow move speed
        arrowSpeed = Mathf.Clamp(Mathf.Pow(1.05f, difficulty), 1f, 7.5f);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        SetDifficulty();

        if (timer < arrowGenTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(arrowMinGenTime - arrowGenTimeRandomRange, arrowMinGenTime);

            Vector3 arrowPos;
            do
            {
                arrowPos = randEdgePosition();
            } while ((arrowPos - hero.transform.position).magnitude < 1f);
            
            ArrowMove arrowMoveScript = ((GameObject)Instantiate(arrow, arrowPos, arrow.transform.rotation)).GetComponent<ArrowMove>();
            arrowMoveScript.hero = hero;
            arrowMoveScript.gameController = this;
            arrowMoveScript.speed = arrowSpeed;
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
