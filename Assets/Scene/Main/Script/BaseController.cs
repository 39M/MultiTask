using UnityEngine;
using System.Collections;

public abstract class BaseController : MonoBehaviour
{
    public bool isLeft;
    public KeyCode keyUp, keyDown, keyRight, keyLeft;

    public virtual void Start()
    {
        if (isLeft)
        {
            keyUp = KeyCode.W;
            keyDown = KeyCode.S;
            keyLeft = KeyCode.A;
            keyRight = KeyCode.D;
        }
        else
        {
            keyUp = KeyCode.UpArrow;
            keyDown = KeyCode.DownArrow;
            keyLeft = KeyCode.LeftArrow;
            keyRight = KeyCode.RightArrow;
        }
    }
}
