using UnityEngine;
using System.Collections;
using System;

public class Title : BaseScene
{
    bool start = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void StartGame(int mode)
    {
        GlobalProperty.mode = mode;
        start = true;
    }

    public void Mute()
    {
        GlobalProperty.mute = !GlobalProperty.mute;
    }

    public void More()
    {
        Debug.Log("Nothing more.");
    }

    public override bool FadeOutCondition()
    {
        return start;
    }

    public override void AfterFadeOut()
    {
        Application.LoadLevel("Main");
    }
}
