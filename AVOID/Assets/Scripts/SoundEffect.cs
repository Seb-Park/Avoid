using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffect : MonoBehaviour {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = Mathf.Abs(PlayerPrefs.GetInt("isSFX")-1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
