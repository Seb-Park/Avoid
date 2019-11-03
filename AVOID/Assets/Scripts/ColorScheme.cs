using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScheme : MonoBehaviour
{
    public Color backGroundColor, wallColor, defaultTextColor;

    public Sprite backGroundSprite; 

    public ColorScheme(Color bg, Color wc, Color tc){
        backGroundColor = bg;
        wallColor = wc;
        defaultTextColor = tc;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
