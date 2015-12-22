using UnityEngine;
using System.Collections;

public class EatIt : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool EatItdegameover;
    float time1,time2,time3;
    public bool isVisible;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            time2 = Time.time;
            Debug.Log(time2);
            time3 = time2 - time1;
            Debug.Log(time3);
        }
        else if(isVisible)
        {
            EatItdegameover = true;
        }

        if (time3 > 0.1f)
            EatItdegameover = true;    
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("pocky"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Canteat"))
        {
            time1 = Time.time;
            Debug.Log(time1);
        }
    }
}
        
