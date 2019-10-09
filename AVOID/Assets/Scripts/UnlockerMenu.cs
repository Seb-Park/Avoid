using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockerMenu : MonoBehaviour
{
    public string[] names;
    public Sprite[] sprites;

    public Animator vaultAnimator;
    public Image newSkin;
    public Text newSkinName;
    public Text newOrOld;

    public SkinManager sm;

    public int[] weights;


    // Start is called before the first frame update
    void Start()
    {
        vaultAnimator.enabled = false;
        newSkinName.text = "";
        newOrOld.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock(){
        if (PlayerPrefs.GetInt("gems") >= 100)
        {
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
            newSkin.sprite = sprites[unlockedIndex]; //set the correct sprite
            newSkinName.text = names[unlockedIndex];


            PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") - 100); 
            vaultAnimator.enabled = true;
            SaveSystem.SaveBoolArray(sm.unlockedSkins, "skins.seb");
            sm.gameObject.SetActive(wasSMOpenBefore);

        }
    }
}
