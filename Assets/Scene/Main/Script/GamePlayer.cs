using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayer : MonoBehaviour
{
    /*
        To add a new minigame:
        - Create a gameobject and then make it a prefab
        - Bind the prefab with a script inherit from BaseGame
        - The script should contain all minigame behavior 
        - Add the prefab to Games
        - Add the script type to GameType
    */

    // All games' gameobject, add new game in inspector
    public GameObject[] Games;
    // All games' type(class), match the Games above
    System.Type[] GameType = { typeof(Balance), typeof(Helicopter), typeof(JumpOverBlock), typeof(AvoidArrow), typeof(EatCube),
    typeof(KeepItUp), typeof(PutBlock), typeof(EvadeTrackingMissile), };

    // Left and right gameobject
    GameObject LeftGamePlayer, RightGamePlayer;
    // Left and right game class
    BaseGame LeftGame, RightGame;
    // Left and right game type index
    int LeftGameType, RightGameType;
    // Difficulty
    float difficulty = 0f;

    // Game switch rate
    float switchTime = 10f;

    // Cover for fade
    public GameObject fadeCover;
    float fadeTime = 0.4f;
    GameObject LeftCover, RightCover;
    SpriteRenderer LeftCoverRenderer, RightCoverRenderer;
    Color LeftCoverColor, RightCoverColor;

    // Timer using for switch game
    float switchTimer = 0;
    // Switch which?
    bool swithLeft = false;
    // Game over flag
    bool gameover = false;
    // Game over timer
    float gameoverTimer = -1f;
    // Which game over first, left or right?
    int gameoverFirst = 0;
    // Game over blink direction;
    bool blinkOut = true;
    // Blink time
    int blinkTime = 3;
    // Game over, start fade out flag
    bool fadeOutStart = false;
    // Score timer
    float scoreTimer = 0;

    // World edge
    public float startX, endX;
    public float startY, endY;
    // 1/4 of screen width
    public float offset;

    // Initialization
    void Start()
    {
        // Init
        Vector3 startVector = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        startX = startVector.x;
        startY = startVector.y;
        Vector3 endVector = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        endX = endVector.x;
        endY = endVector.y;
        offset = (endX - startX) / 4.0f;

        // Difficulty setting
        switch (GlobalProperty.mode)
        {
            case 1:
                difficulty = 10;
                break;
            case 2:
                difficulty = 20;
                break;
            default:
                difficulty = 0;
                break;
        }

        // Init fade cover
        LeftCover = Instantiate(fadeCover);
        LeftCover.transform.Translate(-offset - 0.03f, 0, 0);
        LeftCover.transform.localScale = new Vector3(50 * (endX - startX), 100 * (endY - startY), 1);
        LeftCoverRenderer = LeftCover.GetComponent<SpriteRenderer>();
        LeftCoverColor = LeftCoverRenderer.color;
        RightCover = Instantiate(fadeCover);
        RightCover.transform.Translate(offset + 0.03f, 0, 0);
        RightCover.transform.localScale = new Vector3(50 * (endX - startX), 100 * (endY - startY), 1);
        RightCoverRenderer = RightCover.GetComponent<SpriteRenderer>();
        RightCoverColor = RightCoverRenderer.color;

        // Init game
        LeftGameType = Random.Range(0, Games.Length);
        StartGame(true, LeftGameType);
        RightGameType = -1;
        // RightGame = null;

        scoreTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
            return;

        switchTimer += Time.deltaTime;

        // Game over 
        if ((LeftGame && LeftGame.isGameOver()) || (RightGame && RightGame.isGameOver()))
        {
            if (gameoverFirst == 0)
            {
                scoreTimer = Time.time - scoreTimer;
                if (LeftGame.isGameOver())
                {
                    gameoverFirst = -1;
                    if (RightGame)
                        RightGame.gameover = true;
                }
                else
                {
                    gameoverFirst = 1;
                    if (LeftGame)
                        LeftGame.gameover = true;
                }
            }

            if (blinkTime <= 0)
            {
                if (!fadeOutStart)
                {
                    fadeOutStart = true;
                    LeftCover.transform.Translate(0.03f, 0, 0);
                    RightCover.transform.Translate(-0.03f, 0, 0);

                    if (!LeftGame)
                    {
                        LeftCoverColor.a = 0;
                        LeftCoverRenderer.color = LeftCoverColor;
                    }

                    if (!RightGame)
                    {
                        RightCoverColor.a = 0;
                        RightCoverRenderer.color = RightCoverColor;
                    }
                }

                // Fade out
                gameoverTimer += Time.deltaTime;
                if (gameoverTimer > 0 && gameoverTimer < fadeTime + 0.1f)
                {
                    Fade(1);
                }
                else if (gameoverTimer >= fadeTime + 0.1f)
                {
                    GameOver();
                }
            }
            else
            {
                // Blink
                if (gameoverFirst == -1)
                {
                    // Left
                    if (blinkOut)
                    {
                        Fade(1, -1, 2f);
                        if (LeftCoverColor.a >= 1f)
                            blinkOut = !blinkOut;
                    }
                    else
                    {
                        Fade(-1, -1, 2f);
                        if (LeftCoverColor.a <= 1e-9)
                        {
                            blinkOut = !blinkOut;
                            blinkTime--;
                        }
                    }
                }

                if (gameoverFirst == 1)
                {
                    // Right
                    if (blinkOut)
                    {
                        Fade(1, 1, 2f);
                        if (RightCoverColor.a >= 1f)
                            blinkOut = !blinkOut;
                    }
                    else
                    {
                        Fade(-1, 1, 2f);
                        if (RightCoverColor.a <= 1e-9)
                        {
                            blinkOut = !blinkOut;
                            blinkTime--;
                        }
                    }
                }
            }
            return;
        }

        // Switch game by switchTime
        if (switchTimer > switchTime)
        {
            difficulty += 1f;
            if (swithLeft)
            {
                LeftGameType = NextGameType(new int[] { LeftGameType, RightGameType });
                SwitchGame(true, LeftGameType);
            }
            else
            {
                RightGameType = NextGameType(new int[] { RightGameType, LeftGameType });
                SwitchGame(false, RightGameType);
            }
            swithLeft = !swithLeft;
            switchTimer = 0;
        }

        // Fade in
        if (switchTimer < fadeTime + 0.1f)
            if (!swithLeft)
                Fade(-1, -1);
            else
                Fade(-1, 1);


        // Fade out
        if (switchTimer > switchTime - fadeTime - 0.1f)
            if (swithLeft)
                Fade(1, -1);
            else
                Fade(1, 1);
    }

    // Get next game type
    int NextGameType(int[] preGameType = null)
    {
        if (preGameType == null)
            return Random.Range(0, Games.Length);

        int nextGameType;
        bool repeat;
        do
        {
            repeat = false;
            nextGameType = Random.Range(0, Games.Length);
            foreach (var pre in preGameType)
            {
                if (pre == nextGameType)
                    repeat = true;
            }
        } while (repeat);
        return nextGameType;
    }

    // Fade
    void Fade(int direction, int which = 0, float speedUp = 1)
    {
        /* 
            direction: -1 - fade in, 1 - fade out
            which: -1 - left only, 1 - right only, other - both
        */

        if (which != 1)
        {
            // Left fade
            LeftCoverColor.a += direction * 1.0f / fadeTime * Time.deltaTime * speedUp;

            if (direction == 1)
            {
                if (LeftCoverColor.a > 1f)
                    LeftCoverColor.a = 1f;
            }

            if (direction == -1)
            {
                if (LeftCoverColor.a < 0f)
                    LeftCoverColor.a = 0f;
            }

            LeftCoverRenderer.color = LeftCoverColor;
        }

        if (which != -1)
        {
            // Right fade
            RightCoverColor.a += direction * 1.0f / fadeTime * Time.deltaTime * speedUp;

            if (direction == 1)
            {
                if (RightCoverColor.a > 1f)
                    RightCoverColor.a = 1f;
            }

            if (direction == -1)
            {
                if (RightCoverColor.a < 0f)
                    RightCoverColor.a = 0f;
            }

            RightCoverRenderer.color = RightCoverColor;
        }
    }

    // Switch left or right game to a new game by ID
    void SwitchGame(bool isLeft, int ID)
    {
        EndGame(isLeft);
        StartGame(isLeft, ID);
    }

    // Start a new left or right game by ID
    void StartGame(bool isLeft, int ID)
    {
        if (isLeft)
        {
            LeftGamePlayer = Instantiate(Games[ID]);
            LeftGame = LeftGamePlayer.GetComponent(GameType[ID]) as BaseGame;
            LeftGame.isLeft = true;
            LeftGame.difficulty = difficulty;
        }
        else
        {
            RightGamePlayer = Instantiate(Games[ID]);
            RightGame = RightGamePlayer.GetComponent(GameType[ID]) as BaseGame;
            RightGame.isLeft = false;
            RightGame.difficulty = difficulty;
        }
    }

    // End left or right game
    void EndGame(bool isLeft)
    {
        if (isLeft && LeftGame)
        {
            LeftGame.End();
            Destroy(LeftGamePlayer);
        }
        else if (!isLeft && RightGame)
        {
            RightGame.End();
            Destroy(RightGamePlayer);
        }
    }

    // End all games
    void GameOver()
    {
        gameover = true;
        EndGame(true);
        EndGame(false);

        GlobalProperty.finalScore = scoreTimer;
        if (!PlayerPrefs.HasKey("BestScore") || PlayerPrefs.GetFloat("BestScore") < scoreTimer)
            PlayerPrefs.SetFloat("BestScore", scoreTimer);

        Debug.Log("Survive Time: " + scoreTimer);
        Application.LoadLevel("GameOver");
    }
}
