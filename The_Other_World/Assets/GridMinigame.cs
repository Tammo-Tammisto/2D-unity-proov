using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMinigame : MonoBehaviour
{
    public static List<string> correctGridValues = new List<string> { "7", "11", "20" };
    private static List<string> selectedGridValues = new List<string>();

    private static GridMinigame instance;

    public AudioSource Remove;
    public AudioSource Add;

    void Awake()
    {
        // Ensure only one instance exists across scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        correctGridValues.Sort();
        UpdateGridVisuals();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect mouse click
        {
            DetectGridClick();
        }
    }

    void DetectGridClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;

            if (clickedObject.CompareTag("GridCell"))
            {
                string buttonValue = clickedObject.name.Replace("GridCell_", ""); // Extract number
                ToggleGridSelection(buttonValue, clickedObject);
            }
            else if (clickedObject.CompareTag("DifferenceExit"))
            {
                if (IsSelectionCorrect())
                {
                    FindObjectOfType<FadeInOut>().SwitchScene("FinalRoom");
                }
                else
                {
                    FindObjectOfType<FadeInOut>().SwitchScene("FindTheDifference");
                }
            }
        }
    }

    public void ToggleGridSelection(string number, GameObject gridObject)
    {
        SpriteRenderer spriteRenderer = gridObject.GetComponent<SpriteRenderer>();

        if (selectedGridValues.Contains(number))
        {
            selectedGridValues.Remove(number);
            if (spriteRenderer) spriteRenderer.enabled = false; // Hide sprite
            Debug.Log($"Grid {number} deselected. Current selection: {string.Join(", ", selectedGridValues)}");
        }
        else
        {
            selectedGridValues.Add(number);
            if (spriteRenderer) spriteRenderer.enabled = true; // Show sprite
            Debug.Log($"Grid {number} selected. Current selection: {string.Join(", ", selectedGridValues)}");
        }
    }

    public bool IsSelectionCorrect()
    {
        selectedGridValues.Sort();
        return selectedGridValues.SequenceEqual(correctGridValues);
    }

    void UpdateGridVisuals()
    {
        GameObject[] gridCells = GameObject.FindGameObjectsWithTag("GridCell");

        foreach (GameObject cell in gridCells)
        {
            string buttonValue = cell.name.Replace("GridCell_", "");
            SpriteRenderer spriteRenderer = cell.GetComponent<SpriteRenderer>();

            if (spriteRenderer)
            {
                spriteRenderer.enabled = selectedGridValues.Contains(buttonValue);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (Remove != null)
        {
            Remove.Play();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateGridVisuals();
        if (Add != null)
        {
            Remove.Play();
        }
    }
}