using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class KeypadManager : MonoBehaviour
{
    public TMP_Text displayText; // Assign this in Inspector
    private string enteredCode = "";
    public string correctCode = "6942"; // Set the correct passcode

    public AudioSource keyPressAudioSource; // AudioSource for keypress sounds
    public AudioSource successAudioSource; // AudioSource for success sound

    void Start()
    {
        displayText.text = ""; // Clear display at start
    }

    void Update()
    {
        // Check for mouse input
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            OnMouseDown();
        }
    }
    // Detect button clicks using 2D Physics
    void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        Debug.Log(hit);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.CompareTag("KeypadButton"))
            {
                string buttonValue = clickedObject.name.Replace("Button_", ""); // Extract number from button name
                AddNumber(buttonValue);
                PlayKeyPressSound(); // Play sound when a key is pressed
            }
            else if (clickedObject.CompareTag("KeypadSubmit"))
            {
                SubmitCode();
                PlayKeyPressSound(); // Play sound for the submit button
            }
            else if (clickedObject.CompareTag("KeypadClear"))
            {
                ClearInput();
                PlayKeyPressSound(); // Play sound for the clear button
            }
            else if(clickedObject.CompareTag("KeypadExit"))
            {
                FindObjectOfType<FadeInOut>().SwitchScene("Game"); // Load the game scene
                PlayKeyPressSound(); // Play sound for the exit button
            }
        }
    }

    public void AddNumber(string number)
    {
        if (enteredCode.Length < 4) // Limit input length
        {
            enteredCode += number;
            displayText.text = enteredCode;
        }
    }

    public void ClearInput()
    {
        enteredCode = "";
        displayText.text = "";
    }

    public void SubmitCode()
    {
        if (enteredCode == correctCode)
        {
            Debug.Log("Access Granted! Door Unlocks!");
            PlaySuccessSound(); // Play success sound
            // Perform any action like opening a door
            PlayerController.isDoorUnlocked = true; // Set door unlocked state
            FindObjectOfType<FadeInOut>().SwitchScene("Game");
        }
        else
        {
            Debug.Log("Incorrect Code! Try Again.");
            ClearInput();
        }
    }

    private void PlayKeyPressSound()
    {
        if (keyPressAudioSource != null)
        {
            keyPressAudioSource.Play();
        }
    }

    private void PlaySuccessSound()
    {
        if (successAudioSource != null)
        {
            successAudioSource.Play();
        }
    }
}