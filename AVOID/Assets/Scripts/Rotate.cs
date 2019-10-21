using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float rotation, rotationz;
    public Obstacle obstacle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!obstacle.frozen)
        {
            rotationz += rotation;
            gameObject.transform.eulerAngles = new Vector3(0, 0, rotationz);
        }
	}
}
