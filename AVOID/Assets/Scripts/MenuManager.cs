using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Text gemCount;
    public RectTransform skinsMenu, skinsMenuScrolledImage;
    public RectTransform optionsMenu;
    public RectTransform skinUnlocker;
    public SkinManager skinManager;
    public GameObject ratingPanel;


    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("timesOpenedSinceRating", PlayerPrefs.GetInt("timesOpenedSinceRating") + 1);
        if(PlayerPrefs.GetInt("timesOpenedSinceRating") >= 15 && PlayerPrefs.GetInt("hasRated")<1)//if you've opened the menu 30 or more times and you haven't rated the game yet
        {
            openWindow(ratingPanel);
            PlayerPrefs.SetInt("timesOpenedSinceRating", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        gemCount.text = PlayerPrefs.GetInt("gems").ToString();
	}

    public void startGame(){
        //Debug.Log("Trying to loadScene");
        SceneManager.LoadScene(1);
    }

    public void showSkins(){
        skinsMenu.anchoredPosition = new Vector3(0, 0, 0);
        skinManager.currentHoveringSkin = PlayerPrefs.GetInt("skin");
        skinsMenuScrolledImage.anchoredPosition = new Vector3(0, -skinManager.skinsPositions[PlayerPrefs.GetInt("skin")],0);
        skinManager.currentHoveringSkin = PlayerPrefs.GetInt("skin");
        skinsMenu.gameObject.SetActive(true);
    }

    public void hideSkins(){
        skinsMenu.anchoredPosition = new Vector3(800, 0, 0);
        skinsMenuScrolledImage.anchoredPosition = new Vector3(0, -skinManager.skinsPositions[PlayerPrefs.GetInt("skin")], 0);
        skinsMenu.gameObject.SetActive(false);
    }


    public void showUnlocker()
    {
        skinUnlocker.anchoredPosition = new Vector3(0, 0, 0);
        skinUnlocker.gameObject.SetActive(true);
    }

    public void hideUnlocker()
    {
        skinUnlocker.anchoredPosition = new Vector3(-800, 0, 0);
        skinUnlocker.gameObject.SetActive(false);
    }

    public void showOptions()
    {
        optionsMenu.anchoredPosition = new Vector3(0, 0, 0);
        optionsMenu.gameObject.SetActive(true);
    }

    public void hideOptions()
    {
        optionsMenu.anchoredPosition = new Vector3(-800, 0, 0);
        optionsMenu.gameObject.SetActive(false);
    }

    public void addGems(int gemsToAdd){
        PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") + gemsToAdd);
    }

    public void closeWindow(GameObject go){
        go.SetActive(false);
    }

    public void openWindow(GameObject go)
    {
        go.SetActive(true);
    }

    public void openRatings(){
        Application.OpenURL("market://details?id=" + Application.identifier);
        PlayerPrefs.SetInt("hasRated", 1);
    }
    public void longVibrate(){
        Handheld.Vibrate();
    }
    public void shortVibrate(){
        Vibration.Vibrate(50);
    }
}
