using UnityEngine;
using System.Collections;
using System;

public class Title : BaseScene
{
    bool startFadeOut = false;
    bool startGame = false;
    bool exitGame = false;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
            ExitGame();
            
    }

    public void StartGame(int mode)
    {
        GlobalProperty.mode = mode;
        startFadeOut = true;
        startGame = true;
    }

    public void Mute()
    {
        GlobalProperty.mute = !GlobalProperty.mute;
    }

    public void More()
    {
        Debug.Log("Nothing more.");
    }

    public void ExitGame()
    {
        startFadeOut = true;
        exitGame = true;
    }

    public override bool FadeOutCondition()
    {
        return startFadeOut;
    }

    public override void AfterFadeOut()
    {
        if (startGame)
            Application.LoadLevel("Main");

        if (exitGame)
            Application.Quit();
    }
}
