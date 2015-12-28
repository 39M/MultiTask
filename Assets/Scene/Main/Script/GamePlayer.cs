using UnityEngine;
using System.Collections;

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
    // Game over flag
    bool gameover = false;
    // Game over timer
    float gameoverTimer = 0;

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

        // Init fade cover
        LeftCover = Instantiate(fadeCover);
        LeftCover.transform.Translate(-offset, 0, 0);
        LeftCover.transform.localScale = new Vector3(50 * (endX - startX), 100 * (endY - startY), 1);
        LeftCoverRenderer = LeftCover.GetComponent<SpriteRenderer>();
        LeftCoverColor = LeftCoverRenderer.color;
        RightCover = Instantiate(fadeCover);
        RightCover.transform.Translate(offset, 0, 0);
        RightCover.transform.localScale = new Vector3(50 * (endX - startX), 100 * (endY - startY), 1);
        RightCoverRenderer = RightCover.GetComponent<SpriteRenderer>();
        RightCoverColor = RightCoverRenderer.color;

        // Init game
        LeftGameType = Random.Range(0, Games.Length);
        RightGameType = NextGameType(LeftGameType);
        StartGame(true, LeftGameType);
        StartGame(false, RightGameType);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
            return;

        switchTimer += Time.deltaTime;

        if (LeftGame.isGameOver() || RightGame.isGameOver())
        {
            if (gameoverTimer < fadeTime + 0.1f)
            {
                // Fade out
                gameoverTimer += Time.deltaTime;
                Fade(1);
            }
            else
            {
                GameOver();
            }
        }

        // Switch game by switchTime
        if (switchTimer > switchTime)
        {
            LeftGameType = NextGameType(LeftGameType);
            RightGameType = NextGameType(RightGameType, LeftGameType);
            SwitchGame(true, LeftGameType);
            SwitchGame(false, RightGameType);
            switchTimer = 0;
        }

        // Fade in
        if (switchTimer < fadeTime + 0.1f)
            Fade(-1);

        // Fade out
        if (switchTimer > switchTime - fadeTime - 0.1f)
            Fade(1);
    }

    // Get next game type
    int NextGameType(int gameTypeNow, int gameTypeAnother = -1)
    {
        int nextGameType;
        do
        {
            nextGameType = Random.Range(0, Games.Length);
        } while (nextGameType == gameTypeNow || nextGameType == gameTypeAnother);
        return nextGameType;
    }

    // Fade
    void Fade(int direction)
    {
        LeftCoverColor.a += direction * 1.0f / fadeTime * Time.deltaTime;
        RightCoverColor.a += direction * 1.0f / fadeTime * Time.deltaTime;

        if (direction == 1)
        {
            if (LeftCoverColor.a > 1f)
                LeftCoverColor.a = 1f;

            if (RightCoverColor.a > 1f)
                RightCoverColor.a = 1f;
        }

        if (direction == -1)
        {
            if (LeftCoverColor.a < 0f)
                LeftCoverColor.a = 0f;

            if (RightCoverColor.a < 0f)
                RightCoverColor.a = 0f;
        }


        LeftCoverRenderer.color = LeftCoverColor;
        RightCoverRenderer.color = RightCoverColor;
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
        }
        else
        {
            RightGamePlayer = Instantiate(Games[ID]);
            RightGame = RightGamePlayer.GetComponent(GameType[ID]) as BaseGame;
            RightGame.isLeft = false;
        }
    }

    // End left or right game
    void EndGame(bool isLeft)
    {
        if (isLeft)
        {
            LeftGame.End();
            Destroy(LeftGamePlayer);
        }
        else
        {
            RightGame.End();
            Destroy(RightGamePlayer);
        }
    }

    // End all games
    void GameOver()
    {
        gameover = true;
        scoreTimer = Time.time;
        Debug.Log("Survive Time: " + scoreTimer);
        EndGame(true);
        EndGame(false);
    }
}
