using UnityEngine;
using System.Collections;

public class OneThirdHeroController : OneThirdController
{
    Vector3 position;

    public override void Start()
    {
        base.Start();
        position = transform.position;
    }
    
    public override void Update()
    {
        if (baseGame.gameover || baseGame.destroy)
            return;

        base.Update();

        if (Input.GetKeyDown(keyLeft) || touchLeft)
        {
            position.x = baseGame.startX + (baseGame.endX - baseGame.startX) / 6f;
            transform.position = position;
        }

        if (Input.GetKeyDown(keyUp) || Input.GetKeyDown(keyDown) || touchMiddle)
        {
            position.x = baseGame.startX + (baseGame.endX - baseGame.startX) / 2f;
            transform.position = position;
        }

        if (Input.GetKeyDown(keyRight) || touchRight)
        {
            position.x = baseGame.startX + (baseGame.endX - baseGame.startX) * 5 / 6f;
            transform.position = position;
        }
    }
}
