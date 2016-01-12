using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour
{
    public Text ScoreText;
    public Text BestScoreText;


    // Use this for initialization
    void Start()
    {
        ScoreText.text = GlobalProperty.finalScore.ToString("N2");
        BestScoreText.text = "Best: " + PlayerPrefs.GetFloat("BestScore").ToString("N2");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Retry()
    {
        Application.LoadLevel("Main");
    }

    public void Back()
    {
        Application.LoadLevel("Title");
    }
}
