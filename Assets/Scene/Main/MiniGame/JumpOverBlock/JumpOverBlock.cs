using UnityEngine;

public class JumpOverBlock : BaseGame
{
    public GameObject rect;
    public GameObject land;
    public GameObject block;
    float timer = float.MaxValue;

    public override void Start()
    {
        base.Start();

        rect = CreateGameObjectWithRatio(rect, 0.2f);
        rect.GetComponent<RectController>().isLeft = isLeft;
        rect.GetComponent<RectController>().gameController = this;

        land = CreateGameObject(land);
        land.transform.localScale = new Vector3((endX - startX) * 100f, 20, 1);
    }

    public override void Update()
    {
        if (destroy || gameover)
            return;

        timer += Time.deltaTime;

        if (timer > 3f * Mathf.Pow(0.9f, difficulty))
        {
            var b = CreateGameObjectWithRatio(block, 0.95f);
            // If at right, move block behind the left cover
            if (!isLeft)
                b.transform.Translate(0, 0, 5);
            b.transform.localScale = new Vector3(Mathf.Clamp(Random.Range(50, 100) * Mathf.Pow(1.025f, difficulty), 0, 200), 50, 50);
            b.GetComponent<BlockMove>().gameController = this;
            b.GetComponent<BlockMove>().moveSpeed = Mathf.Clamp(2.5f * Mathf.Pow(1.04f, difficulty), 0, 7.5f);
            timer = Random.Range(-0.75f - 2 * Mathf.Pow(0.95f, difficulty), -0.75f);
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
