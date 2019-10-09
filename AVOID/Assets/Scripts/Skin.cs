using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour {

    public int skinNumber, price;
    public SkinManager sm;
    public RectTransform rt;
    public int screenHeight;
    public float rtYPos;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
        ResizeByScale();
        if(rt.localScale.x > .99f){
            sm.currentHoveringSkin = skinNumber;
        }
    }

    void ResizeByRT(){
        rtYPos = rt.position.y;

        rt.sizeDelta = new Vector2(150, 50000 / Mathf.Abs(rt.position.y - (screenHeight / 2)));

        if (rt.sizeDelta.y > 150)
        {
            rt.sizeDelta = new Vector2(150, 150);
        }
    }

    void ResizeByScale(){
        rtYPos = rt.position.y;

        rt.localScale = new Vector2((screenHeight / 10) / Mathf.Abs(rt.position.y - (screenHeight / 2)), (screenHeight / 10) / Mathf.Abs(rt.position.y - (screenHeight / 2)));

        if (rt.localScale.y > 1)
        {
            rt.localScale = new Vector2(1, 1);
        }

        if (rt.localScale.y < .5f)
        {
            rt.localScale = new Vector2(.5f, .5f);
        }
    }

    public void OnButtonClickOld(){
        if (sm.unlockedSkins[skinNumber])
        {//check the unlocked array in the skin manager to see if this skin is unlocked 
            PlayerPrefs.SetInt("skin", skinNumber);
        }
        else{
            if(PlayerPrefs.GetInt("gems")>=price){
                PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") - price);
                sm.unlockedSkins[skinNumber] = true;
                PlayerPrefs.SetInt("skin", skinNumber);
                SaveSystem.SaveBoolArray(sm.unlockedSkins, "skins.seb");
            }
        }
    }

    public void OnButtonClick(){
        sm.skinsParentScroll.velocity = new Vector2(0, 0);
        sm.currentHoveringSkin = skinNumber;
        sm.skinsParentImage.anchoredPosition = new Vector2(0, -sm.skinsPositions[skinNumber]);

        //if (skinNumber < sm.currentHoveringSkin)
        //{
        //    sm.skinsParentImage.anchoredPosition = new Vector2(0, -sm.skinsPositions[skinNumber] + 125);
        //}else{
        //    sm.skinsParentImage.anchoredPosition = new Vector2(0, -sm.skinsPositions[skinNumber] - 125);
        //}
        sm.currentHoveringSkin = skinNumber;

    }
}
