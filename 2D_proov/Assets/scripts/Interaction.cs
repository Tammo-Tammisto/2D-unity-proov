using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Canvas EPromptCanvas;
    public Canvas TutorialCanvas;
    public Canvas PointCanvas;
    public Canvas StartCanvas;
    public GameObject PromptArea;
    public RainSpawner Spawner;

    private bool playerInTrigger = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            EPromptCanvas.enabled = true;
            Debug.Log("Player entered trigger area!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            EPromptCanvas.enabled = false;
            TutorialCanvas.enabled = false;
        }
    }

    void Update()
    {
        if (playerInTrigger)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("E key pressed");
                EPromptCanvas.enabled = false;
                TutorialCanvas.enabled = true;
                StartCanvas.enabled = false;
            }

            if (TutorialCanvas.enabled == true)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    TutorialCanvas.enabled = false;
                    PointCanvas.enabled = true;
                    Debug.Log(gameObject.name + " has been interacted with.");
                    PromptArea.SetActive(false);
                    if (Spawner != null)
                    {
                        Spawner.StartRaining();
                    }
                }
            }
        }
    }
}