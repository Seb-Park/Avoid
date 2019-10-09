using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraBGColor : MonoBehaviour {

    public Color[] colors;

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().backgroundColor = colors[PlayerPrefs.GetInt("theme")];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
