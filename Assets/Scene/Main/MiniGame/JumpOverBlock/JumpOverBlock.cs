﻿using UnityEngine;

public class JumpOverBlock : BaseGame
{
    public GameObject rect;
    public GameObject land;
    public GameObject block;
    float timer = 0;

    public override void Start()
    {
        base.Start();

        rect = Instantiate(rect);
        rect.transform.Translate(offset, 0, 0);

        land = Instantiate(land);
        land.transform.Translate(offset, 0, 0);
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1.5f)
        {
            var b = Instantiate(block);
            // If at right, move block behind the left cover
            if (isLeft)
                b.transform.Translate(offset, 0, 0);
            else
                b.transform.Translate(offset, 0, 5);
            b.GetComponent<BlockMove>().gameController = this;
            timer = 0;
        }
    }

    public override void End()
    {
        gameover = true;
        Destroy(rect);
        Destroy(land);
        Destroy(gameObject);
    }
}