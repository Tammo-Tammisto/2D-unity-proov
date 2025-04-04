using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BlueKeyPad : MonoBehaviour
{
    public TMP_Text displayText; // Assign this in Inspector
    private string enteredCode = "";
    public string correctCode = "212215"; // Set the correct passcode

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
            }
            else if (clickedObject.CompareTag("KeypadSubmit"))
            {
                SubmitCode();
            }
            else if (clickedObject.CompareTag("KeypadClear"))
            {
                ClearInput();
            }
            else if (clickedObject.CompareTag("KeypadExit"))
            {
                FindObjectOfType<FadeInOut>().SwitchScene("Level 3"); // Load the game scene
            }
        }
    }

    public void AddNumber(string number)
    {
        if (enteredCode.Length < 6) // Limit input length
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
            // Perform any action like opening a door
            PlayerController.isDoorUnlocked = true; // Set door unlocked state
            FindObjectOfType<FadeInOut>().SwitchScene("Level 3");
        }
        else
        {
            Debug.Log("Incorrect Code! Try Again.");
            ClearInput();
        }
    }
}
