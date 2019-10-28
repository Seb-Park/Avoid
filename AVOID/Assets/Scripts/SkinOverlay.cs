using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinOverlay : MonoBehaviour
{
    public Sprite selected, locked, unlockable, available;
    public Image imageComponent;
    public int index;
    public SkinManager sm;
    public GameObject parent;
    public Skin parentSkin;

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        parentSkin = parent.GetComponent<Skin>();
        index = parentSkin.skinNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("skin") == index)//if you're using this skin
        {
            imageComponent.sprite = selected;
        }
        else if(sm.unlockedSkins[index]){
            imageComponent.sprite = available;
        }
        else if(!sm.unlockedSkins[index]&&GetComponentInParent<Skin>().price> PlayerPrefs.GetInt("gems")){
            imageComponent.sprite = locked;
        }
        else{
            imageComponent.sprite = locked;
        }
    }
}
