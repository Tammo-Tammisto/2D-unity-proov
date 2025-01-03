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
    void Start()
    {
        InfoCanvas.enabled = true;
        DeathCanvas.enabled = false;
        Time.timeScale = 1;
        Score.score = 0;
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
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
    }

    void FixedUpdate()
    {
        transform.Translate(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, 0);
    }

    public void TakeDamage(float damage)
    {
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
