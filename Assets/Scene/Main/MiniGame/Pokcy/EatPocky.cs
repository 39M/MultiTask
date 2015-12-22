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
       

        CreateGameObject(pocky);
        ei = CreateGameObject(mouth).GetComponentInChildren<EatIt>();
        
    }

    public override void Update()
    {
        if (ei.EatItdegameover)
            gameover = true;
    }

    public override void End()
    {
        Destroy(mouth);
        Destroy(pocky);
        Destroy(gameObject);
    }
}
