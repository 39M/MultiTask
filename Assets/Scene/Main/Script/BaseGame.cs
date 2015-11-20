using UnityEngine;
using System.Collections;

public abstract class BaseGame : MonoBehaviour
{
    public bool gameover;
    public bool isLeft;
    public float offset;

    // Initialization
    public abstract void Start();

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
