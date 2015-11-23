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

    // Initialization
    public virtual void Start()
    {
        if (isLeft)
            offset = -4.4f;
        else
            offset = 4.4f;
    }

    // Update is called once per frame
    public abstract void Update();

    // Check if game over
    public bool isGameOver()
    {
        return gameover;
    }

    // Remove
    public abstract void End();
}
