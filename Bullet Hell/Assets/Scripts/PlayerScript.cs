using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public static float health = 4f;
    public Canvas InfoCanvas;
    public Canvas DeathCanvas;
    public AudioSource DefaultMusic;
    public AudioSource DeathMusic;
    float horizontalMove, verticalMove;
    public float deathScore;
    public TMP_Text deathScoreText;
    public UnityEngine.UI.Image hp1, hp2, hp3, hp4;
    private bool isPaused = false;

    // Dash variables
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float invincibilityDuration = 0.5f;
    private bool isDashing = false;
    private bool isInvincible = false;
    private float dashTimeRemaining = 0f;
    private float invincibilityTimeRemaining = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        InfoCanvas.enabled = true;
        DeathCanvas.enabled = false;
        Time.timeScale = 1;
        health = 4;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            verticalMove = Input.GetAxisRaw("Vertical") * speed;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
        }

        HandleTimers();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(horizontalMove, verticalMove);
        }
    }

    void Dash()
    {
        isDashing = true;
        isInvincible = true;
        dashTimeRemaining = dashDuration;
        invincibilityTimeRemaining = invincibilityDuration;

        if (horizontalMove != 0 || verticalMove != 0)
        {
            // Dash in the movement direction
            rb.velocity = new Vector2(horizontalMove, verticalMove).normalized * dashSpeed;
        }
        else
        {
            // If not moving, remain stationary but invincible
            rb.velocity = Vector2.zero;
        }
    }

    void HandleTimers()
    {
        if (isDashing)
        {
            dashTimeRemaining -= Time.deltaTime;
            if (dashTimeRemaining <= 0)
            {
                isDashing = false;
                rb.velocity = Vector2.zero; // Stop dash
            }
        }

        if (isInvincible)
        {
            invincibilityTimeRemaining -= Time.deltaTime;
            if (invincibilityTimeRemaining <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            Score.score += 100;
            return;
        } // Ignore damage when invincible
        Score.score -= 899;
        health -= damage;
        UpdateHealthUI();

        if (health <= 0)
        {
            Pause();
            InfoCanvas.enabled = false;
            DefaultMusic.Stop();
            DeathCanvas.enabled = true;
            deathScore = Score.score;
            deathScoreText.text = "Score: " + deathScore + "PTS";
            DeathMusic.Play();
        }
    }

    void UpdateHealthUI()
    {
        hp1.enabled = health >= 1;
        hp2.enabled = health >= 2;
        hp3.enabled = health >= 3;
        hp4.enabled = health >= 4;
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
    }
}