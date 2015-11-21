using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour {
    public Helicopter gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-5 * Time.deltaTime, 0, 0);
        if (gameController.gameover || transform.position.x < -4.4 + gameController.offset)
            Destroy(gameObject);
	}
}
