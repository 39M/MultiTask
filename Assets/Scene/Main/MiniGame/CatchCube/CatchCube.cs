using UnityEngine;
using System.Collections;

public class CatchCube : BaseGame
{
    public GameObject hero;
    public GameObject cube;

    float cubeGenTime;
    float cubeMinGenTime;
    float cubeGenTimeRandomRange;
    float cubeSpeed;

    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        hero = CreateGameObjectWithRatio(hero, 0.5f, 0.025f);
        hero.GetComponent<CatchCubeHeroController>().baseGame = this;
        hero.GetComponent<CatchCubeHeroController>().isLeft = isLeft;
        hero.transform.localScale = new Vector3((endX - startX) * 25f, 20);
    }

    public void SetDifficulty()
    {
        // Generate time between two block
        cubeGenTime = 4f * Mathf.Pow(0.915f, difficulty);
        // Min generate time
        cubeMinGenTime = -0.15f;
        // Random range
        cubeGenTimeRandomRange = Mathf.Pow(0.9f, difficulty);
        // Arrow move speed
        cubeSpeed = Mathf.Clamp(Mathf.Pow(1.05f, difficulty), 0, 7.5f);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        SetDifficulty();

        if (timer < cubeGenTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(cubeMinGenTime - cubeGenTimeRandomRange, cubeMinGenTime);

            CubeMove cubeMoveScript = CreateGameObjectWithRatio(cube, 1 / 6f + 1 / 3f * Random.Range(0, 3), 1.1f).GetComponent<CubeMove>();
            cubeMoveScript.baseGame = this;
            cubeMoveScript.speed = cubeSpeed;
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(hero);
        Destroy(gameObject);
    }
}
