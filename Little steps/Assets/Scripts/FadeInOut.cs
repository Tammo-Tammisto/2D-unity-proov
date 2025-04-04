using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public bool fadein = false;
    public bool fadeout = false;
    public float fadeSpeed;

    // Update is called once per frame
    void Update()
    {
        if (fadein == true)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            }
        }
        if (fadeout == true)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            }
        }
    }
    
    public void FadeIn()
    {
        fadein = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }

    public void FadeInFalse()
    {
        canvasGroup.alpha = 1;
        fadein = false;
    }

    public void FadeOutFalse()
    {
        canvasGroup.alpha = 0;
        fadeout = false;
    }
}
