using UnityEngine;
using System.Collections;

public class Helicopter : BaseGame {
    public GameObject limit;
    public GameObject plane;
    public GameObject block;
    float timer = 0;

    public override void Start()
    {
        if (isLeft)
            offset = -4.4f;
        else
            offset = 4.4f;

        limit = Instantiate(limit);
        limit.transform.Translate(offset, 0, 0);

        plane = Instantiate(plane);
        plane.transform.Translate(offset, 0, 0);
    }

    public override void Update()
    {        
        timer += Time.deltaTime;

        // Generate a new block
        if (timer > 1)
        {
            var b = Instantiate(block);
            b.transform.Translate(offset, Random.Range(-3f, 3f), 0);
            b.GetComponent<BlockMove>().gameController = this;
            timer = 0;
        }
    }

    public override void End()
    {
        gameover = true;
        Destroy(limit);
        Destroy(plane);
        Destroy(this);        
    }
}
