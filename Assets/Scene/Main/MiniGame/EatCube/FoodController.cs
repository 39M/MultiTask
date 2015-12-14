using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour
{
    public BaseGame gameController;
    SpriteRenderer render;
    Color foodColor;
    float fadeTime = 0.25f;
    const float totalTime = 10f;
    float originScale;
    float leftTime;
    bool fullyShow = false;
    bool eaten = false;

    // Use this for initialization
    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        foodColor = render.color;
        foodColor.a = 0;
        render.color = foodColor;
        leftTime = totalTime;
        originScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gameover)
            Destroy(gameObject);

        leftTime -= Time.deltaTime;
        if (leftTime < 0)
            gameController.gameover = true;

        float deltaScale = Time.deltaTime * (originScale / totalTime);
        gameObject.transform.localScale -= new Vector3(deltaScale, deltaScale, 0);

        // Fade out
        if (eaten)
        {
            if (foodColor.a <= 1e-5)
                Destroy(gameObject);

            foodColor.a -= 1.0f / fadeTime * Time.deltaTime;
            render.color = foodColor;
        }

        // Fade in
        if (!fullyShow)
        {
            foodColor.a += 1.0f / fadeTime * Time.deltaTime;
            render.color = foodColor;
            if (foodColor.a >= 1)
                fullyShow = true;
        }
        else
        {
            fullyShow = true;
        }
    }

    void Eaten()
    {
        eaten = true;
    }    
}
