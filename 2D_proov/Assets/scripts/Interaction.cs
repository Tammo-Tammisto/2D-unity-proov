using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Canvas EPromptCanvas;
    public Canvas TutorialCanvas;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered!");
        if(other.CompareTag("Player"))
        {
            EPromptCanvas.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            EPromptCanvas.enabled = false;
            TutorialCanvas.enabled = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                Debug.Log("E key pressed");
                EPromptCanvas.enabled = false;
                TutorialCanvas.enabled = true;
            }
            if(Input.GetKey(KeyCode.X))
            {
                TutorialCanvas.enabled = false;
                EPromptCanvas.enabled = true;
            }
        }
    }
}
