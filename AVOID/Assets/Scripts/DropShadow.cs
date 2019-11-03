using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropShadow : MonoBehaviour {

    public Vector3 offset = new Vector3(.1f, -.1f);
    public GameObject shadow;
    public Material material;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.GetInt("isShadows") < 1)
        {
            createFixedShadow();
        }
        else{
            enabled = false;
        }
    }

    public void createDynamicShadow(){
        shadow = new GameObject("shadow");
        //shadow.transform.parent = gameObject.transform;
        shadow.transform.position = gameObject.transform.position + offset;
        shadow.transform.rotation = Quaternion.identity;
        shadow.transform.localScale = transform.localScale;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = shadow.AddComponent<SpriteRenderer>(); //making a renderer for this one.
        sr.sprite = renderer.sprite; //makes shadow the same shape as this sprite
        sr.material = material;
        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = -10;
        Color black = new Color(0, 0, 0);
        sr.color = black;
    }

    public void createFixedShadow(){
        shadow = new GameObject("shadow");
        shadow.transform.parent = gameObject.transform;
        shadow.transform.localPosition = offset;
        shadow.transform.localRotation = Quaternion.identity;
        shadow.transform.localScale = new Vector3(1, 1, 1);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sr = shadow.AddComponent<SpriteRenderer>(); //making a renderer for this one.
        sr.sprite = renderer.sprite; //makes shadow the same shape as this sprite
        sr.material = material;
        sr.sortingLayerName = renderer.sortingLayerName;
        sr.sortingOrder = -10;
        Color black = new Color(0, 0, 0);
        sr.color = black;
    }
	
    public void moveFixedShadow(){
        shadow.transform.localPosition = offset;
    }

    public void moveDynamicShadow(){
        shadow.transform.position = gameObject.transform.position + offset;
    }

    // Update is called once per frame
    void Update () {
        moveFixedShadow();
    }
}
