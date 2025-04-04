using TMPro;
using UnityEngine;
using System.Collections;  // For coroutines

public class Dialogue : MonoBehaviour
{
    public TMP_Text myText; // Assign in Inspector
    public float fadeSpeed = 1.0f;
    public float delayBeforeFadeOut = 2.0f;  // Time in seconds before fading out

    private bool fadeIn = false;
    private bool fadeOut = false;

    void Start()
    {
        // Ensure outline starts at 0 (fully visible)
        if (myText != null)
        {
            myText.outlineWidth = 1;  // Start with visible text (outline width = 0)
        }
    }

    void Update()
    {
        // Handle fade-in (outline going from 0 to 1 to make it invisible)
        if (fadeIn)
        {
            if (myText.outlineWidth > 0) // Increase outline width to 1 (make it invisible)
            {
                myText.outlineWidth -= fadeSpeed * Time.deltaTime;
            }
            else
            {
                myText.outlineWidth = 0;
                fadeIn = false; // Stop increasing when max is reached
                StartCoroutine(FadeOutAfterDelay()); // Start fade-out after delay
            }
        }
        // Handle fade-out (outline going from 1 to 0 to make it visible again)
        else if (fadeOut)
        {
            if (myText.outlineWidth < 1) // Decrease outline width to 0 (make it visible)
            {
                myText.outlineWidth += fadeSpeed * Time.deltaTime;
            }
            else
            {
                fadeOut = false; // Stop decreasing when min is reached
                myText.outlineWidth = 1; // Ensure it stays fully visible
                ResetFade();
            }
        }
    }

    // Call this to start the fade-in effect
    public void StartFadeIn(string text)
    {
        if (myText != null)
        {
            myText.text = text; // Set the text
            myText.outlineWidth = 1; // Start with visible text (outline width = 0)
            fadeIn = true;
            fadeOut = false; // Ensure we don't fade out while fading in
        }
    }

    // Fade out after a delay
    private IEnumerator FadeOutAfterDelay()
    {
        // Wait for the specified delay before starting fade-out
        yield return new WaitForSeconds(delayBeforeFadeOut);
        fadeOut = true; // Start fading out after the delay
    }

    // Reset the fade effect (outline back to fully visible)
    public void ResetFade()
    {
        if (myText != null)
        {
            myText.outlineWidth = 1; // Ensure outline is reset to visible (outline width = 0)
        }
        fadeIn = false;
        fadeOut = false;
    }
}