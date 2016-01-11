using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PutBlock : BaseGame
{
    public GameObject block;
    public GameObject countdownBar;
    SpriteRenderer countdownBarRenderer;
    Color countdownBarColor;
    int col_count = 3;
    public bool success = false;
    float countDown = 20f;
    int max_block;
    int[] blocks_count;
    List<List<GameObject>> all_blocks = new List<List<GameObject>>();

    public override void Start()
    {
        base.Start();

        max_block = (int)difficulty + 5;

        // Ensure col_count won't over flow
        col_count = Mathf.Clamp(col_count, 1, 3);

        var blockScale = new Vector3(200, ((endY - startY) * 80) / (max_block + 1), 1);

        // Create countdown bar
        countdownBar = CreateGameObjectWithRatio(countdownBar, 0.5f, 0.025f);
        countdownBar.transform.localScale = new Vector3(650, 10, 1);
        countdownBarRenderer = countdownBar.GetComponent<SpriteRenderer>();
        countdownBarColor = countdownBarRenderer.color;


        // Randomly create blocks
        blocks_count = new int[col_count];
        for (int i = 0; i < col_count; i++)
        {
            all_blocks.Add(new List<GameObject>());
            int block_num = Random.Range(1, max_block - (int)Mathf.Floor(50f / blockScale.y));
            for (int j = 0; j < block_num; j++)
            {
                float percentageX = (i + 1) / (col_count + 1f);
                // position + half scale + countdownBar space                
                float percentageY = j / (max_block + 1f) + blockScale.y / 2 / ((endY - startY) * 100) + 0.05f;
                var blk = CreateGameObjectWithRatio(block, percentageX, percentageY);
                blk.transform.localScale = blockScale;
                all_blocks[i].Add(blk);
            }
        }
    }

    public override void Update()
    {
        // Game over
        if (gameover)
        {
            return;
        }

        if (col_count != 1 && Input.GetKeyDown(keyLeft))
        {
            if (all_blocks[0].Count <= blocks_count[0])
            {
                gameover = true;
                return;
            }
            all_blocks[0][blocks_count[0]].GetComponent<SpriteRenderer>().color = Color.black;
            blocks_count[0]++;
        }

        if (col_count != 2 && Input.GetKeyDown(keyUp))
        {
            int index_t = col_count == 1 ? 0 : 1;
            if (all_blocks[index_t].Count <= blocks_count[index_t])
            {
                gameover = true;
                return;
            }
            all_blocks[index_t][blocks_count[index_t]].GetComponent<SpriteRenderer>().color = Color.black;
            blocks_count[index_t]++;
        }

        if (col_count != 1 && Input.GetKeyDown(keyRight))
        {
            int index_t = col_count == 3 ? 2 : 1;
            if (all_blocks[index_t].Count <= blocks_count[index_t])
            {
                gameover = true;
                return;
            }
            all_blocks[index_t][blocks_count[index_t]].GetComponent<SpriteRenderer>().color = Color.black;
            blocks_count[index_t]++;
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
        countdownBar.transform.localScale = new Vector3(countDown / 20 * 650, 10, 1);
        if (countDown < 0)
        {
            gameover = true;
            return;
        }

        // Check if success
        success = true;
        for (int i = 0; i < col_count; i++)
            if (all_blocks[i].Count != blocks_count[i])
                success = false;
    }

    public override void End()
    {
        destroy = true;
        foreach (var l in all_blocks)
            foreach (var g in l)
                Destroy(g);
        Destroy(countdownBar);
        Destroy(gameObject);
    }
}
