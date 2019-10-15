using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrozenTimeCanvas : MonoBehaviour
{
    public float startTime;
    //public Text timeText;
    public Image bg;
    public Slider slider;
    public int duration;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeLeft = (duration - (Time.time - startTime));
        Color fullBgColor = bg.color;
        fullBgColor.a = timeLeft / duration;
        bg.color = fullBgColor;
        slider.value = timeLeft / duration;
        //timeText.text = (int)(timeLeft+1) + "s";
    }
}
