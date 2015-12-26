using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PutBlock : BaseGame
{
    public GameObject block;
    public GameObject countdownBar;
    SpriteRenderer countdownBarRenderer;
    Color countdownBarColor;
    public int col_count = 2;
    public bool success = false;
    float countDown = 8f;
    int max_block = 9;
    int[] blocks_count;
    List<List<GameObject>> all_blocks = new List<List<GameObject>>();

    public override void Start()
    {
        base.Start();

        // Create countdown bar
        countdownBar = CreateGameObjectWithRatio(countdownBar, 0.5f, 0.03f);
        countdownBarRenderer = countdownBar.GetComponent<SpriteRenderer>();
        countdownBarColor = countdownBarRenderer.color;

        // Randomly create blocks
        blocks_count = new int[col_count];
        for (int i = 0; i < col_count; i++)
        {
            all_blocks.Add(new List<GameObject>());
            int block_num = Random.Range(2, max_block);
            for (int j = 0; j < block_num; j++)
            {
                float percentageX = (i + 1) / (col_count + 1f);
                float percentageY = (j + 1) * 0.1f;
                all_blocks[i].Add(CreateGameObjectWithRatio(block, percentageX, percentageY));
            }
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(keyLeft))
        {
            if (all_blocks[0].Count <= blocks_count[0])
            {
                gameover = true;
                return;
            }
            all_blocks[0][blocks_count[0]].GetComponent<SpriteRenderer>().color = Color.black;
            blocks_count[0]++;
        }

        if (Input.GetKeyDown(keyRight))
        {
            if (all_blocks[1].Count <= blocks_count[1])
            {
                gameover = true;
                return;
            }
            all_blocks[1][blocks_count[1]].GetComponent<SpriteRenderer>().color = Color.black;
            blocks_count[1]++;
        }

        // Game over, set success false, make countdown bar red
        if (gameover)
        {
            success = false;

            if (countdownBarColor.r < 1f)
                countdownBarColor.r += Time.deltaTime * 5;

            if (countdownBarColor.g > 0f)
                countdownBarColor.g -= Time.deltaTime * 5;

            if (countdownBarColor.b > 0f)
                countdownBarColor.b -= Time.deltaTime * 5;

            if (countdownBarColor.a < 1f)
                countdownBarColor.a += Time.deltaTime * 5;

            countdownBarRenderer.color = countdownBarColor;
            return;
        }

        // Success, fade out countdown bar
        if (success)
        {
            if (countdownBarColor.a > 0f)
            {
                countdownBarColor.a -= Time.deltaTime * 2;
                countdownBarRenderer.color = countdownBarColor;
            }
            return;
        }

        // Countdown
        countDown -= Time.deltaTime;
        countdownBar.transform.localScale = new Vector3(countDown / 8 * 500, 10, 1);
        if (countDown < 0)
        {
            gameover = true;
            return;
        }

        // Check if success
        if (all_blocks[0].Count == blocks_count[0] && all_blocks[1].Count == blocks_count[1])
            success = true;
    }

    public override void End()
    {
        foreach (var l in all_blocks)
            foreach (var g in l)
                Destroy(g);
        Destroy(countdownBar);
        Destroy(gameObject);
    }
}
