using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeSprites : MonoBehaviour
{
    public string spriteNames;
    private SpriteRenderer spriteR;
    public Sprite[] spritesChange;

    Light2D globalLight;
    float intensity;

    void Start()
    {
       
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        globalLight = GameObject.Find("Global Light").GetComponent<Light2D>();

    }
    private void Update()
    {
        if(GameManager.Instance.saveData.completeFuel && gameObject.name.Equals("Fuel Point"))
        {
            spriteR.sprite = spritesChange[0];
        }

        //if (GameManager.Instance.saveData.completeCP && gameObject.name.Equals("Control Panel Repair Point"))
        //{
        //    spriteR.sprite = spritesChange;
        //}

        if(GameManager.Instance.saveData.completeCP && GameManager.Instance.saveData.completeFuel)
        {
            if(!gameObject.name.Equals("Fuel Point") && !gameObject.name.Equals("Control Panel Repair Point"))
            {
                spriteR.sprite = spritesChange[0];
            }

            globalLight.intensity = 1f;
        }

    }
}
