using TMPro;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static float _moveSpeed = 5f;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";

    public AudioSource footstepAudioSource; // Reference to the AudioSource for footstep sounds
    public AudioClip footstepClip; // Footstep sound clip
    public float footstepInterval = 0.5f; // Time interval between footsteps

    private float _footstepTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.velocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);

        HandleFootstepSounds();
    }

    private void HandleFootstepSounds()
    {
        if (_movement.magnitude > 0.1f) // Check if the player is moving
        {
            _footstepTimer -= Time.deltaTime;
            if (_footstepTimer <= 0f)
            {
                PlayFootstepSound();
                _footstepTimer = footstepInterval; // Reset the timer
            }
        }
        else
        {
            _footstepTimer = 0f; // Reset the timer when not moving
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepAudioSource != null && footstepClip != null)
        {
            footstepAudioSource.PlayOneShot(footstepClip);
        }
    }
}