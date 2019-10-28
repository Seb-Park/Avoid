using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    public SkinManager sm;
    public Image selectButtonImage;
    public Sprite selectedImage, selectableImage, unlockableImage, lockedImage;
    public Text buttonText;

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("skin")==sm.currentHoveringSkin){
            selectButtonImage.sprite = selectedImage;
            buttonText.text = "Selected";
        }
        else if(sm.unlockedSkins[sm.currentHoveringSkin]){
            selectButtonImage.sprite = selectableImage;
            buttonText.text = "Select";

        }
        else {
            selectButtonImage.sprite = unlockableImage;
            buttonText.text = "";
        }
    }

    public void OnButtonClick()
    {
        if (sm.unlockedSkins[sm.currentHoveringSkin])
        {//check the unlocked array in the skin manager to see if this skin is unlocked 
            PlayerPrefs.SetInt("skin", sm.currentHoveringSkin);
        }
        else
        {
            //if (PlayerPrefs.GetInt("gems") >= sm.skinObjects[sm.currentHoveringSkin].price)
            //{
            //    PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") - sm.skinObjects[sm.currentHoveringSkin].price);
            //    sm.unlockedSkins[sm.currentHoveringSkin] = true;
            //    PlayerPrefs.SetInt("skin", sm.currentHoveringSkin);
            //    SaveSystem.SaveBoolArray(sm.unlockedSkins, "skins.seb");
            //}
            //*****************************************************************************
            //deduct a magic key from inventory
            //if the player has no magic keys open the shop
        }
    }
}
