using UnityEngine;
using System.Collections;

public class MainUI : BaseScene
{
    public GamePlayer gamePlayer;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void AfterFadeOut()
    {
        gamePlayer.GameOver();
    }

    public override bool FadeOutCondition()
    {
        return gamePlayer.fadeOutStart;
    }
}
