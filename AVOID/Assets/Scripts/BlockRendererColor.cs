using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRendererColor : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ColorManager colorManager;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        colorManager = GameObject.FindWithTag("ColorManager").GetComponent<ColorManager>();
        spriteRenderer.color = colorManager.getBlockColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
