using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public float ypos;
    public float speed = 0.02f;
    public float startY=15f;
    public Vector3 destructionPoint = new Vector3(0,-20,0);
    public GameObject gm;
    public bool frozen;


	// Use this for initialization
	void Start () {
        //gm = GameObject.Find("GameManager");
        gm = GameObject.FindGameObjectWithTag("GameController");
        ypos = startY;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(0, ypos, 0);

    }

    void FixedUpdate()
    {
        if (gm.GetComponent<GameManager>().isStarted&&!frozen) {
            ypos -= speed*Time.timeScale;
            //ypos -= speed;
            if (transform.position.y < destructionPoint.y)
            {
                Destroy(gameObject);
            }
        }

    }
}
