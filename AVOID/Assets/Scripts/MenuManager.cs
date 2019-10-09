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


    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
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
        skinsMenuScrolledImage.anchoredPosition = new Vector3(0, -skinManager.skinsPositions[PlayerPrefs.GetInt("skin")],0);
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
}
