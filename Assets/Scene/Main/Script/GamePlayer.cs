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
    System.Type[] GameType = { typeof(Balance), };
    
    // Left and right gameobject
    GameObject LeftGamePlayer, RightGamePlayer;
    // Left and right game class
    BaseGame LeftGame, RightGame;
    // Left and right game type index
    int LeftGameType, RightGameType;
    
    // Timer using for switch game
    float Timer = 0;
    // Game over flag
    bool gameover = false;

    // Initialization
    void Start()
    {
        //Debug.Log(Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 10)));
        //Debug.Log(Camera.main.WorldToViewportPoint(new Vector3(100, 100, 0)));

        LeftGameType = Random.Range(0, Games.Length);
        RightGameType = Random.Range(0, Games.Length);
        StartGame(true, LeftGameType);
        StartGame(false, RightGameType);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
            return;

        Timer += Time.deltaTime;

        if (LeftGame.isGameOver() || RightGame.isGameOver())
        {
            GameOver();
        }

        // Switch game every ?? seconds
        if (Timer > 5f)
        {
            LeftGameType = Random.Range(0, Games.Length);
            RightGameType = Random.Range(0, Games.Length);
            SwitchGame(true, LeftGameType);
            SwitchGame(false, RightGameType);
            Timer = 0;
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
        EndGame(true);
        EndGame(false);
    }
}
