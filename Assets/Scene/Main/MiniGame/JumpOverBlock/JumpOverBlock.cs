using UnityEngine;

public class JumpOverBlock : BaseGame
{
    public GameObject rect;
    public GameObject land;
    public GameObject block;
    float timer = float.MaxValue;

    float blockGenTime;
    float blockMinGenTime;
    float blockGenTimeRandomRange;
    float blockLength;
    float blockMoveSpeed;

    public override void Start()
    {
        base.Start();

        rect = CreateGameObjectWithRatio(rect, 0.2f);
        rect.GetComponent<RectController>().isLeft = isLeft;
        rect.GetComponent<RectController>().baseGame = this;

        land = CreateGameObject(land);
        land.transform.localScale = new Vector3((endX - startX) * 100f, 20, 1);
    }

    public void SetDifficulty()
    {
        blockGenTime = Mathf.Pow(0.95f, difficulty);
        blockMinGenTime = -0.3f;
        blockGenTimeRandomRange = Mathf.Clamp(2 * Mathf.Pow(0.95f, difficulty), 0.3f, float.MaxValue);
        blockLength = Random.Range(50, 100) * Mathf.Clamp(Mathf.Pow(1.025f, difficulty), 1f, 2f);
        blockMoveSpeed = Mathf.Clamp(2.5f * Mathf.Pow(1.035f, difficulty), 0, 7.5f);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        SetDifficulty();
        timer += Time.deltaTime;

        if (timer > 3f * blockGenTime)
        {
            var b = CreateGameObjectWithRatio(block, 0.95f);
            // If at right, move block behind the left cover
            if (!isLeft)
                b.transform.Translate(0, 0, 5);
            b.transform.localScale = new Vector3(blockLength, 50, 50);
            b.GetComponent<BlockMove>().gameController = this;
            b.GetComponent<BlockMove>().moveSpeed = blockMoveSpeed;
            timer = Random.Range(blockMinGenTime - blockGenTimeRandomRange, blockMinGenTime);
        }
    }

    public override void End()
    {
        destroy = true;
        Destroy(rect);
        Destroy(land);
        Destroy(gameObject);
    }
}
