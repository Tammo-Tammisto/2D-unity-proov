using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animated : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public Transform targetObject; // The object to zoom in on
    public float zoomSpeed = 2f; // Speed of the zoom
    public Vector3 targetScale = new Vector3(31f, 31f, 11f); // Target scale for the zoom (31, 31, 11)
    public Transform cameraTransform; // Reference to the camera transform
    public Vector3 hotspotPosition; // The position to zoom in on
    public AudioSource animationSound; 
    
    public AudioSource doorbang;// Public AudioSource to play after stopping all sounds

    private bool shouldZoom = false;

    IEnumerator PlayDoorbangWithDelay()
    {
        yield return new WaitForSeconds(0.7f); // Wait for 2 seconds
        if (doorbang != null)
        {
            doorbang.Play();
        }

        yield return new WaitForSeconds(0.3f);
        if (animationSound != null)
        {
            animationSound.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource assigned to animationSound.");
        }
    }

    void Start()
    {
        StopAllSounds(); // Stop all sounds when the animation starts

        // Play the specific sound attached to this script

        // Ensure the Animator is assigned
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        // Ensure the cameraTransform is assigned
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        // Start looping the animation
        LoopAnimation("grass");

        // Start coroutine to play doorbang with delay
        StartCoroutine(PlayDoorbangWithDelay());
    }

    void Update()
    {
        // Check if the animation has finished
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && !animator.IsInTransition(0))
        {
            // Trigger zoom when the animation ends
            if (!shouldZoom)
            {
                OnAnimationEnd();
            }
        }

        // Check if we should start zooming
        if (shouldZoom)
        {
            // Smoothly move the camera toward the hotspot position
            cameraTransform.position = Vector3.Lerp(
                cameraTransform.position,
                hotspotPosition,
                zoomSpeed * Time.deltaTime
            );

            // Smoothly scale the object to the target scale (31, 31, 11)
            targetObject.localScale = Vector3.Lerp(
                targetObject.localScale,
                targetScale,
                zoomSpeed * Time.deltaTime
            );

            // Stop zooming when the target scale and position are reached
            if (Vector3.Distance(cameraTransform.position, hotspotPosition) < 0.01f &&
                Vector3.Distance(targetObject.localScale, targetScale) < 0.01f)
            {
                shouldZoom = false;
            }
        }
    }

    // This method should be called when the animation ends
    public void OnAnimationEnd()
    {
        shouldZoom = true;
    }

    // Function to loop an animation on the GameObject
    public void LoopAnimation(string animationName)
    {
        if (!string.IsNullOrEmpty(animationName))
        {
            animator.Play(animationName, 0, 0f); // Play the animation from the start
        }
    }

    void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Stop();
        }
    }

}