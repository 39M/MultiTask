using UnityEngine;
using System.Collections;

public class DodgeBlock : BaseGame
{
    public GameObject hero;
    public GameObject block;

    float blockGenTime;
    float blockMinGenTime;
    float blockGenTimeRandomRange;
    float blockSpeed;

    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        hero = CreateGameObjectWithRatio(hero, 0.5f, 0.05f);
        hero.GetComponent<OneThirdHeroController>().baseGame = this;
        hero.GetComponent<OneThirdHeroController>().isLeft = isLeft;
        hero.transform.localScale = new Vector3(50, 50);
    }

    public void SetDifficulty()
    {
        // Generate time between two block
        blockGenTime = 4f * Mathf.Pow(0.915f, difficulty);
        // Min generate time
        blockMinGenTime = -0.15f;
        // Random range
        blockGenTimeRandomRange = Mathf.Pow(0.9f, difficulty);
        // Arrow move speed
        blockSpeed = Mathf.Clamp(Mathf.Pow(1.05f, difficulty), 0, 7.5f);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        SetDifficulty();

        if (timer < blockGenTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = Random.Range(blockMinGenTime - blockGenTimeRandomRange, blockMinGenTime);

            GameObject blk = CreateGameObjectWithRatio(block, 1 / 6f + 1 / 3f * Random.Range(0, 3), 1.1f);
            blk.transform.localScale = new Vector3((endX - startX) * 25f, 20);
            DodgeBlockBlockMove blockMoveScript = blk.GetComponent<DodgeBlockBlockMove>();
            blockMoveScript.baseGame = this;
            blockMoveScript.speed = blockSpeed;
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(hero);
        Destroy(gameObject);
    }
}
