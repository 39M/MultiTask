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
		// Get screen edge
        Vector3 startVector, endVector;
        startVector = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        startX = startVector.x;
        startY = startVector.y;
        endVector = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        endX = endVector.x;
        endY = endVector.y;

		// Set fade cover scale dynamically
        var canvasRect = gameObject.GetComponent<RectTransform>().rect;
        var coverRect = FadeCover.GetComponent<RectTransform>();
        coverRect.sizeDelta = new Vector2(canvasRect.width, canvasRect.height);
        FadeCoverColor = FadeCover.color;
    }

    public virtual void Update()
    {
		// Fade in
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

		// Fade out
        if (FadeOutCondition())
        {
            FadeCover.enabled = true;

            if (FadeCoverColor.a >= 1f)
                fadeOutDone = true;

            FadeCoverColor.a += Time.deltaTime * 2.5f;
            FadeCover.color = FadeCoverColor;
        }

		// After fade out
        if (fadeOutDone)
        {
            AfterFadeOut();
        }
    }

	// If return true, fade out
    public abstract bool FadeOutCondition();
	// Do something after fade out complete
    public abstract void AfterFadeOut();
}
