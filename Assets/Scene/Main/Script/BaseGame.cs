using UnityEngine;
using System.Collections;

public abstract class BaseGame : MonoBehaviour
{
    // Game difficulty
    public float difficulty;
    // Game destroy flag
    public bool destroy;
    // Game over flag
    public bool gameover;
    // Display at left?
    public bool isLeft;
    // Display position offset
    public float offset;
    // World edge
    public float startX, endX;
    public float startY, endY;
    // Screen edge
    public int screenStartX, screenMiddleX, screenEndX;
    public int screenStartY, screenMiddleY, screenEndY;
    public int screenWidth, screenHeight;
    // Key code
    public KeyCode keyUp, keyDown, keyRight, keyLeft;

    // Get world edge in screen, calculate offset, set control keys
    public virtual void Start()
    {
        gameover = false;

        Vector3 startVector, endVector;
        if (isLeft)
        {
            startVector = new Vector3(0, 0);
            endVector = new Vector3(0.5f, 1);

            screenStartX = 0;
            screenMiddleX = Screen.width / 4;
            screenEndX = Screen.width / 2;

            keyUp = KeyCode.W;
            keyDown = KeyCode.S;
            keyLeft = KeyCode.A;
            keyRight = KeyCode.D;
        }
        else
        {
            startVector = new Vector3(0.5f, 0);
            endVector = new Vector3(1, 1);

            screenStartX = Screen.width / 2;
            screenMiddleX = Screen.width * 3 / 4;
            screenEndX = Screen.width;

            keyUp = KeyCode.UpArrow;
            keyDown = KeyCode.DownArrow;
            keyLeft = KeyCode.LeftArrow;
            keyRight = KeyCode.RightArrow;
        }

        screenStartY = 0;
        screenMiddleY = Screen.height / 2;
        screenEndY = Screen.height;
        screenWidth = screenEndX - screenStartX;
        screenHeight = screenEndY - screenStartY;

        startVector = Camera.main.ViewportToWorldPoint(startVector);
        startX = startVector.x;
        startY = startVector.y;
        endVector = Camera.main.ViewportToWorldPoint(endVector);
        endX = endVector.x;
        endY = endVector.y;

        offset = (endX - startX) / 2.0f;

        if (isLeft)
            offset = -offset;
    }

    public virtual void FixedUpdate()
    {

    }

    public virtual void Update()
    {

    }

    // Check if game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Remove all GameObject, remember to set destroy as true
    public abstract void End();

    public GameObject CreateGameObject(GameObject go)
    {
        var t = Instantiate(go);
        t.transform.Translate(offset, 0, 0);
        return t;
    }

    // Create a game object at ratioX and ratioY of game area
    public GameObject CreateGameObjectWithRatio(GameObject go, float ratioX = 0.5f, float ratioY = 0.5f)
    {
        var t = Instantiate(go);
        t.transform.Translate(offset * 2 * (isLeft ? (1 - ratioX) : ratioX), (endY - startY) * (ratioY - 0.5f), 0);
        //t.transform.position = new Vector3(offset * 2 * (isLeft ? (1 - ratioX) : ratioX),
        //(endY - startY) * (ratioY - 0.5f), 0);
        return t;
    }

    // Pos: -1=>floor, 1=>ceiling
    public GameObject CreateHorizontalLimit(GameObject limit, int pos)
    {
        var t = Instantiate(limit);
        t.transform.Translate(offset, pos * ((endY - startY) / 2 + 0.03f), 0);
        t.transform.localScale = new Vector3((endX - startX) * 100f, 6, 1);
        return t;
    }

    public GameObject CreateVertitalLimit(GameObject limit)
    {
        var t = Instantiate(limit);
        t.transform.Translate((isLeft ? -1 : 1) * (endX - startX + 0.03f), 0, 0);
        t.transform.localScale = new Vector3(6, (endY - startY) * 100f, 1);
        return t;
    }
}
