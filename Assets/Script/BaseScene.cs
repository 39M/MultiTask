using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class BaseScene : MonoBehaviour
{
    public float startX, endX;
    public float startY, endY;
    public Image FadeCover;
    Color FadeCoverColor;
    bool fadeInDone = false;
    bool fadeOutDone = false;

    public virtual void Start()
    {
        Vector3 startVector, endVector;
        startVector = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        startX = startVector.x;
        startY = startVector.y;
        endVector = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        endX = endVector.x;
        endY = endVector.y;

        var canvasRect = gameObject.GetComponent<RectTransform>().rect;
        var coverRect = FadeCover.GetComponent<RectTransform>();
        coverRect.sizeDelta = new Vector2(canvasRect.width, canvasRect.height);
        FadeCoverColor = FadeCover.color;
    }

    public virtual void Update()
    {
        if (!fadeInDone)
        {
            if (FadeCoverColor.a <= 0)
            {
                FadeCoverColor.a = 0f;
                FadeCover.enabled = false;
                fadeInDone = true;
            }

            FadeCoverColor.a -= Time.deltaTime * 2.5f;
            FadeCover.color = FadeCoverColor;
        }

        if (FadeOutCondition())
        {
            FadeCover.enabled = true;

            if (FadeCoverColor.a >= 1f)
                fadeOutDone = true;

            FadeCoverColor.a += Time.deltaTime * 2.5f;
            FadeCover.color = FadeCoverColor;
        }

        if (fadeOutDone)
        {
            AfterFadeOut();
        }
    }

    public abstract bool FadeOutCondition();
    public abstract void AfterFadeOut();
}
