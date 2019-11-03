using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public ColorScheme[] colorSchemes;
    public Color[] cameraBGColors;
    public Color[] wallColors;
    public Color[] blockColors;
    public Color[] defaultTextColors;

    public Camera mainCamera;
    public SpriteRenderer[] walls;
    public Text[] defaultTexts;

    private int themeNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        themeNumber = PlayerPrefs.GetInt("theme");
        mainCamera = Camera.main;

        mainCamera.backgroundColor = cameraBGColors[themeNumber];
        foreach(SpriteRenderer sr in walls){
            sr.color = wallColors[themeNumber];
        }

        foreach (Text t in defaultTexts){
            t.color = getTextColor();
        }
    }

    public Color getBlockColor(){
        return blockColors[themeNumber];
    }

    public Color getBGColor()
    {
        return cameraBGColors[themeNumber];
    }

    public Color getTextColor()
    {
        return defaultTextColors[themeNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
