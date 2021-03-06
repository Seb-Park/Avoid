﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockerMenu : MonoBehaviour
{
    //public string[] names;
    public Sprite[] sprites;

    public Animator vaultAnimator;
    public Image newSkin;
    public Text newSkinName;
    public Text newOrOld;

    public Button backButton;

    public GameObject unlockButton;

    public SkinManager sm;

    public int[] weights;


    // Start is called before the first frame update
    void Start()
    {
        newSkinName.text = "";
        newOrOld.text = "";
        for (int i = 0; i < weights.Length; i++){
            if(sm.unlockedSkins[i]){
                //makes it less likely for you to get a skin if you already have it
                weights[i] = (int)(weights[i]/2.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("gems")<100){
            unlockButton.SetActive(false);
        }
        else{
            unlockButton.SetActive(true);
        }
    }

    public IEnumerator DisableUnlock(){
        backButton.interactable = false;
        unlockButton.GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(12);
        unlockButton.GetComponent<Button>().interactable = true;
        backButton.interactable = true;

    }

    public void Unlock(){
        if (PlayerPrefs.GetInt("gems") >= 100)
        {
            StartCoroutine(DisableUnlock());
            bool wasSMOpenBefore = sm.gameObject.activeSelf;
            Debug.Log(sm.gameObject.activeSelf +"is the active state of the sm.");
            sm.gameObject.SetActive(true);
            Debug.Log(sm.gameObject.activeSelf + "is the active state of the sm.");

            int unlockedIndex = CustomMath.WeightedRandomize(weights);//getting a random new skin based on the weights

            if (sm.unlockedSkins[unlockedIndex])
            {
                newOrOld.text = "Old";
            }else{
                newOrOld.text = "New!";
            }

            sm.unlockedSkins[unlockedIndex] = true;//unlock it
            Debug.Log(unlockedIndex);
            newSkin.sprite = sprites[unlockedIndex]; //set the correct sprite
            newSkinName.text = sm.names[unlockedIndex];

            //Debug.Log("you first have " + PlayerPrefs.GetInt("gems") + " gems.");
            PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") - 100);
            //Debug.Log("you now have " + PlayerPrefs.GetInt("gems") + " gems.");
            vaultAnimator.SetTrigger("PlayUnlock");
            SaveSystem.SaveBoolArray(sm.unlockedSkins, "skins.seb");
            sm.gameObject.SetActive(wasSMOpenBefore);
        }
    }
}
