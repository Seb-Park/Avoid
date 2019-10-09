using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBoolManager : MonoBehaviour {

    public Text onText;
    public Text offText;
    public Color[] colors;
    public string option;

    // Use this for initialization
    void Start () {
        setButtonColors();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void turnOff(){
        PlayerPrefs.SetInt(option, 1);
        setButtonColors();
    }

    public void turnOn(){
        PlayerPrefs.SetInt(option, 0);
        setButtonColors();
    }
    public void setButtonColors(){
        //0 is on and 1 is off so that the playerpref defaults to on
        //set button colors sets one button to black and the selected one to a highlighted color
        if(PlayerPrefs.GetInt(option)<1){
            onText.color = colors[0];
            //zero is the highlighted color
            offText.color = colors[1];
        }
        else{
            onText.color = colors[1];
            offText.color = colors[0];
        }
    }
}
