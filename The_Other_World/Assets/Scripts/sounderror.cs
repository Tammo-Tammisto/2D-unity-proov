using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class sounderror : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip firstClip;     // First sound clip
    public AudioClip secondClip;    // Second sound clip

    private bool isSecondClipQueued = false;

    private static sounderror instance; // Singleton instance
    private static bool hasStartedPlaying = false; // Tracks if the sound has already started

    void Awake()
    {
        // Ensure only one instance of this GameObject exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist this GameObject across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    void Start()
    {
        // Only play the sound if it hasn't started yet
        if (!hasStartedPlaying)
        {
            if (audioSource != null && firstClip != null)
            {
                // Set the volume to 0.5
                audioSource.volume = 0.5f;

                // Play the first clip
                audioSource.clip = firstClip;
                audioSource.Play();

                hasStartedPlaying = true; // Mark as started
            }
        }
    }

    void Update()
    {
        // Stop the sound if the current scene is not "Level 3" or "BluePuzzleScene"
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Level 3" && currentScene != "BluePuzzleScene")
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop(); // Stop the sound
            }
            return; // Exit the method to prevent further checks
        }

        // Continue playing the second clip if applicable
        if (audioSource != null && !audioSource.isPlaying && !isSecondClipQueued)
        {
            // Play the second clip after the first one finishes
            audioSource.clip = secondClip;
            audioSource.Play();
            isSecondClipQueued = true; // Ensure the second clip is only played once
        }
    }
}
