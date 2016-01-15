using UnityEngine;
using System.Collections;

public abstract class BaseController : MonoBehaviour
{
    public bool isLeft;
    public KeyCode keyUp, keyDown, keyRight, keyLeft;
    public BaseGame baseGame = null;

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

    // Check touch in a horizontally area
    public bool TouchInAreaX(float beginRatio, float endRatio, TouchPhase? phase = null)
    {
        int beginPixel = (int)(baseGame.screenWidth * beginRatio) + baseGame.screenStartX;
        int endPixel = (int)(baseGame.screenWidth * endRatio) + baseGame.screenStartX;
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.x > beginPixel && touch.position.x < endPixel)
                    return true;
        }
        return false;
    }

    // Chech if touch exist
    public bool TouchAnyWhere(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.x > baseGame.screenStartX && touch.position.x < baseGame.screenEndX)
                    return true;
        }
        return false;
    }

    // Check touch in a vertical area
    public bool TouchInAreaY(float beginRatio, float endRatio, TouchPhase? phase = null)
    {
        int beginPixel = (int)(baseGame.screenHeight * beginRatio) + baseGame.screenStartY;
        int endPixel = (int)(baseGame.screenHeight * endRatio) + baseGame.screenStartY;
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.y > beginPixel && touch.position.y < endPixel)
                    return true;
        }
        return false;
    }

    // Check if touch left
    public bool TouchLeft(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.x > baseGame.screenStartX && touch.position.x < baseGame.screenMiddleX)
                    return true;
        }
        return false;
    }

    // Check if touch right
    public bool TouchRight(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.x > baseGame.screenMiddleX && touch.position.x < baseGame.screenEndX)
                    return true;
        }
        return false;
    }

    // Check if touch up
    public bool TouchUp(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.y > baseGame.screenMiddleY && touch.position.y < baseGame.screenEndY)
                    return TouchAnyWhere(phase);
        }
        return false;
    }

    // Check if touch down
    public bool TouchDown(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.y > baseGame.screenStartY && touch.position.y < baseGame.screenMiddleY)
                    return TouchAnyWhere(phase);
        }
        return false;
    }
}
