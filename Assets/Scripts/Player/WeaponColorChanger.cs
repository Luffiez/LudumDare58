using System;
using UnityEngine;

public class WeaponColorChanger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField] private bool UseMaterial;


    [Serializable]
    class ColorChanger
    {
        public float MaxDiff;
        public float TimeOfffset;
        public float Speed;
    }

    [SerializeField]
    ColorChanger RedChanger;
    [SerializeField]
    ColorChanger GreenChanger;
    [SerializeField]
    ColorChanger BlueChanger;

    [SerializeField]
    ColorChanger OpacityChanger;

    float redStartValue = 0f;
    float greenStartValue = 0f;
    float blueStartValue = 0f;
    float opacityStartValue = 0f;
    void Start()
    {
        redStartValue = spriteRenderer.color.r;
        blueStartValue = spriteRenderer.color.b;
        greenStartValue = spriteRenderer.color.g;
        opacityStartValue = spriteRenderer.color.a;
    }

    // Update is called once per frame
    void Update()
    {

        if (UseMaterial)
        {
            spriteRenderer.material.SetColor("_BaseColor", new Color(GetNewColorValue(RedChanger, redStartValue),
            GetNewColorValue(GreenChanger, greenStartValue),
            GetNewColorValue(BlueChanger, blueStartValue),
            GetNewColorValue(OpacityChanger, opacityStartValue)));

        }
        else
        { 
            spriteRenderer.color = new Color(GetNewColorValue(RedChanger, redStartValue),
            GetNewColorValue(GreenChanger, greenStartValue),
            GetNewColorValue(BlueChanger, blueStartValue),
            GetNewColorValue(OpacityChanger, opacityStartValue));
        }

  

    }

    float GetNewColorValue(ColorChanger colorChanger, float startValue)
    {
        return startValue + (Mathf.Sin(Time.time * colorChanger.Speed + colorChanger.TimeOfffset) * colorChanger.MaxDiff);
    }


    void OnDisable()
    {
        if (UseMaterial)
        {
            spriteRenderer.material.SetColor("_BaseColor", new Color(redStartValue, greenStartValue, blueStartValue, opacityStartValue));
        }
        else
        {
            spriteRenderer.color = new Color(redStartValue, greenStartValue, blueStartValue, opacityStartValue);
        }
    }

}
