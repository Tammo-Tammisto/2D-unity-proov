using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    public RainSpawner Spawner;
    public TMP_Text ScoreText;
    public Canvas WinCanvas;
    public Canvas LoseCanvas;
    public AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip dangerSound;
    public AudioClip music;
    public AudioClip rainingMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    private int points;

    private void Start()
    {
        points = 0;
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Spawner.isRaining && audioSource.clip != rainingMusic)
        {
            audioSource.Stop();
            audioSource.clip = rainingMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (WinCanvas.enabled && audioSource.clip != winMusic)
        {
            ScoreText.text = "";
            audioSource.Stop();
            audioSource.PlayOneShot(coinSound);
            audioSource.clip = winMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (!Spawner.isRaining && LoseCanvas.enabled && audioSource.clip != loseMusic)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(dangerSound);
            audioSource.clip = loseMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        if (!Spawner.isRaining && LoseCanvas.enabled == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                points = 0;
                ScoreText.text = points.ToString() + "/15";
                WinCanvas.enabled = false;
                LoseCanvas.enabled = false;
                Spawner.StartRaining();

                if (audioSource.clip != rainingMusic)
                {
                    audioSource.Stop();
                    audioSource.clip = rainingMusic;
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Coin coin = other.gameObject.GetComponent<Coin>();
            if (coin != null && !coin.isCollected)
            {
                coin.isCollected = true; // Mark the coin as collected
                Destroy(other.gameObject);
                points++;
                ScoreText.text = points.ToString() + "/15";
                audioSource.PlayOneShot(coinSound);
                if (points == 15)
                {
                    Debug.Log("You win!");
                    if (Spawner != null)
                    {
                        Spawner.StopRaining();
                    }
                    WinCanvas.enabled = true;
                }
            }
        }
        if (other.gameObject.CompareTag("Danger"))
        {
            Debug.Log("You lose!");
            LoseCanvas.enabled = true;
            if (Spawner != null)
            {
                Spawner.StopRaining();
            }
        }
    }
}
