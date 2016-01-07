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
    // Key code
    public KeyCode keyUp, keyDown, keyRight, keyLeft;

    // Initialization
    public virtual void Start()
    {
        difficulty = 0;
        gameover = false;

        Vector3 startVector, endVector;
        if (isLeft)
        {
            startVector = new Vector3(0, 0);
            endVector = new Vector3(0.5f, 1);
            keyUp = KeyCode.W;
            keyDown = KeyCode.S;
            keyLeft = KeyCode.A;
            keyRight = KeyCode.D;
        }
        else
        {
            startVector = new Vector3(0.5f, 0);
            endVector = new Vector3(1, 1);
            keyUp = KeyCode.UpArrow;
            keyDown = KeyCode.DownArrow;
            keyLeft = KeyCode.LeftArrow;
            keyRight = KeyCode.RightArrow;
        }
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

    // Update is called once per frame
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
