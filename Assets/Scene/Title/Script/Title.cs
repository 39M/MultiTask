using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int mode)
    {
        GlobalProperty.mode = mode;
        Application.LoadLevel("Main");
    }

    public void Mute()
    {
        GlobalProperty.mute = !GlobalProperty.mute;
    }

    public void More()
    {
        Debug.Log("Nothing more.");
    }
}
