using UnityEngine;
using System.Collections;

public abstract class BaseController : MonoBehaviour
{
    public bool isLeft;
    public KeyCode keyUp, keyDown, keyRight, keyLeft;
    public BaseGame gameController = null;

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
        int beginPixel = (int)(gameController.screenWidth * beginRatio) + gameController.screenStartX;
        int endPixel = (int)(gameController.screenWidth * endRatio) + gameController.screenStartX;
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
                if (touch.position.x > gameController.screenStartX && touch.position.x < gameController.screenEndX)
                    return true;
        }
        return false;
    }

    // Check touch in a vertical area
    public bool TouchInAreaY(float beginRatio, float endRatio, TouchPhase? phase = null)
    {
        int beginPixel = (int)(gameController.screenHeight * beginRatio) + gameController.screenStartY;
        int endPixel = (int)(gameController.screenHeight * endRatio) + gameController.screenStartY;
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
                if (touch.position.x > gameController.screenStartX && touch.position.x < gameController.screenMiddleX)
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
                if (touch.position.x > gameController.screenMiddleX && touch.position.x < gameController.screenEndX)
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
                if (touch.position.y > gameController.screenMiddleY && touch.position.y < gameController.screenEndY)
                    return true;
        }
        return false;
    }

    // Check if touch down
    public bool TouchDown(TouchPhase? phase = null)
    {
        foreach (var touch in Input.touches)
        {
            if (phase == null || touch.phase == phase)
                if (touch.position.y > gameController.screenStartY && touch.position.y < gameController.screenMiddleY)
                    return true;
        }
        return false;
    }
}
