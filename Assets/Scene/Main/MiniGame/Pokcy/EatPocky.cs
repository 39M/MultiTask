using UnityEngine;
using System.Collections;
using System;

public class EatPocky : BaseGame
{
    public GameObject mouth;
    public GameObject pocky;
    EatIt ei;

    public override void Start()
    {
        base.Start();

        pocky = CreateGameObject(pocky);
        mouth = CreateGameObject(mouth);
        ei = mouth.GetComponentInChildren<EatIt>();

    }

    public override void Update()
    {
        if (ei.gameover)
            gameover = true;
    }

    public override void End()
    {
        Destroy(mouth);
        Destroy(pocky);
        Destroy(gameObject);
    }
}
