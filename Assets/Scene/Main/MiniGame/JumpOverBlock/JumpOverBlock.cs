using UnityEngine;

public class JumpOverBlock : BaseGame
{
    public GameObject rect;
    public GameObject land;
    public GameObject block;
    float timer = 0;

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

        if (timer > 2f)
        {
            var b = CreateGameObjectWithRatio(block, 0.95f);
            // If at right, move block behind the left cover
            if (!isLeft)
                b.transform.Translate(0, 0, 5);
            b.GetComponent<BlockMove>().gameController = this;
            timer = Random.Range(-1f, 0.5f);
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
