using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameOver : BaseScene
{
    public Text ScoreText;
    public Text BestScoreText;

    bool retry = false;
    bool back = false;

    public override void Start()
    {
        base.Start();

        ScoreText.text = GlobalProperty.finalScore.ToString("N2");
        BestScoreText.text = "Best: " + PlayerPrefs.GetFloat("BestScore").ToString("N2");
    }

    public override void Update()
    {
        base.Update();
        
    }

    public void Retry()
    {
        retry = true;
    }

    public void Back()
    {
        back = true;        
    }

    public override bool FadeOutCondition()
    {
        return retry || back;
    }

    public override void AfterFadeOut()
    {
        if (retry)
            Application.LoadLevel("Main");
        if (back)
            Application.LoadLevel("Title");
    }
}
