using UnityEngine;
using System.Collections;

public class PutBlockController : BaseController
{
    public bool touchLeft, touchMiddle, touchRight;

    void Update()
    {
        touchLeft = TouchInAreaX(0, 1 / 3f, TouchPhase.Began);
        touchMiddle = TouchInAreaX(1 / 3f, 2 / 3f, TouchPhase.Began);
        touchRight = TouchInAreaX(2 / 3f, 1, TouchPhase.Began);
    }
}
