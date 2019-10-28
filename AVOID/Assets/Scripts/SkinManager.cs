using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    public bool[] unlockedSkins;
    public float[] skinsPositions;
    public Skin[] skinObjects;
    public string[] names;
    //public int[] weights;

    public int currentHoveringSkin;
    public ScrollRect skinsParentScroll;
    bool skinsParentScrollIsDragged;
    public RectTransform skinsParentImage;

    public Text nameOfSkin;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < unlockedSkins.Length; i++)
        { //for the length of unlocked skins only set the first one to unlocked
            if (i == 0)
            {
                unlockedSkins[i] = true;

            }
            else
            {
                unlockedSkins[i] = false;
            }
        }

        bool[] loadedArray = SaveSystem.LoadBoolArray("skins.seb");


        if (loadedArray != null){
            for (int i = 0; i < loadedArray.Length; i++){
                unlockedSkins[i] = loadedArray[i];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        SnapScrolls();
    }

    void SnapScrolls(){
        if (!skinsParentScrollIsDragged&&skinsParentScroll.velocity.y<.2f)
        {
            float lerpedPosition = Mathf.Lerp(skinsParentImage.anchoredPosition.y, -skinsPositions[currentHoveringSkin], .2f);
            skinsParentImage.anchoredPosition = new Vector3(0, lerpedPosition, 0);
        }
        nameOfSkin.text = names[currentHoveringSkin];
    }

    public void startDrag(){
        skinsParentScrollIsDragged = true;
    }

    public void endDrag(){
        skinsParentScrollIsDragged = false;
    }

    public void testWeighter(){
        //Debug.Log(names[CustomMath.WeightedRandomize(weights)]);
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveBoolArray(unlockedSkins, "skins.seb");
    }
}
