using UnityEngine;
using System.Collections;
using System;

public class Balance : BaseGame
{
    public GameObject plantform;
    public GameObject ball;

    public override void Start()
    {
        if (isLeft)
            offset = -4.4f;
        else
            offset = 4.4f;
        
        plantform = Instantiate(plantform);
        plantform.GetComponent<HingeJoint2D>().connectedAnchor += new Vector2(offset, 0);

        ball = Instantiate(ball);
        ball.transform.Translate(offset, 0.1f, 0);
    }
    
    public override void Update()
    {
        if (ball.transform.position.y < -5)
            gameover = true;
    }

    public override void End()
    {
        Destroy(plantform);
        Destroy(ball);
        Destroy(this);
    }
}
