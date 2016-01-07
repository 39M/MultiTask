using UnityEngine;
using System.Collections;

public class Mouthmove : MonoBehaviour
{
    bool stop = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            stop = true;
        }

        if (!stop)
            transform.Translate(0, -Time.deltaTime, 0);

    }
}
