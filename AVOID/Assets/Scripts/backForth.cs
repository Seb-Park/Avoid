using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backForth : MonoBehaviour {

    public float x1,x2,dx,xpos;
    public Obstacle obstacle;

	// Use this for initialization
	void Start () {
	}

    void Update()
    {
        transform.position = new Vector3(xpos, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void FixedUpdate () {
        if (!obstacle.frozen)
        {
            xpos += dx;
            if (xpos <= x1 || xpos >= x2)
            {
                dx = -dx;
            }
        }
	}
}
