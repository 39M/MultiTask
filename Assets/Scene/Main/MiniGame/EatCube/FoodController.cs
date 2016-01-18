using UnityEngine;
using System.Collections;

public class FoodController : MonoBehaviour
{
    public BaseGame gameController;
    public float totalTime = 15f;
    SpriteRenderer render;
    Color foodColor;
    float fadeTime = 0.25f;
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
        foodColor.a = 1;

        leftTime = totalTime;
        originScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.destroy)
            Destroy(gameObject);

        if (gameController.gameover)
            return;

        leftTime -= Time.deltaTime;
        if (leftTime < 0)
            gameController.gameover = true;

        // Fade out
        if (eaten)
        {
            if (render.color.a <= 1e-5)
                Destroy(gameObject);
            
            render.color = Color.Lerp(render.color, Color.clear, Time.deltaTime * 10f);
            gameObject.transform.localScale *= 1 + Time.deltaTime * 2f;
        }
        else
        {
            float nextScale = leftTime / totalTime * originScale;
            gameObject.transform.localScale = new Vector3(nextScale, nextScale, 0);
        }

        // Fade in
        if (!fullyShow)
        {
            render.color = Color.Lerp(render.color, foodColor, Time.deltaTime * 5f);
            if (render.color.a >= 0.9999f)
                fullyShow = true;
        }
    }

    void Eaten()
    {
        eaten = true;
        fullyShow = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When collide with block, game over
        if (other.gameObject.CompareTag("Hero"))
            Eaten();
    }
}
