using UnityEngine;
using System.Collections;

public abstract class BaseGame : MonoBehaviour
{
    // Game over flag
    public bool gameover;
    // Display at left?
    public bool isLeft;
    // Display position offset
    public float offset;
    // World edge
    public float startX, endX;
    public float startY, endY;

    // Initialization
    public virtual void Start()
    {
        Vector3 startVector, endVector;
        if (isLeft)
        {
            startVector = new Vector3(0, 0);
            endVector = new Vector3(0.5f, 1);
        }
        else
        {
            startVector = new Vector3(0.5f, 0);
            endVector = new Vector3(1, 1);
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

    // Update is called once per frame
    public virtual void Update()
    {

    }

    // Check if game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Remove
    public abstract void End();

    public GameObject CreateGameObject(GameObject go)
    {
        var t = Instantiate(go);
        t.transform.Translate(offset, 0, 0);
        return t;
    }
}
