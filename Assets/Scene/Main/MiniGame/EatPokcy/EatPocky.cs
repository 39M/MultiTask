using UnityEngine;
using System.Collections;
using System;

public class EatPocky : BaseGame
{

    public override void Start()
    {
        base.Start();

    }

    public void SetDifficulty()
    {

    }

    public override void Update()
    {

    }

    public override void End()
    {
        destroy = true;

        Destroy(gameObject);
    }
}
