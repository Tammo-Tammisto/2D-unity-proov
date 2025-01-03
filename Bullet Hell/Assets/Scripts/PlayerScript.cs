using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public static float health = 100f;
    public Canvas DeathCanvas;
    public AudioSource DefaultMusic;
    public AudioSource DeathMusic;
    float horizontalMove, verticalMove;
    private bool isPaused = false;
    void Start()
    {
        Score.score = 0;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();
            }
        }
        if(health <= 0 && DeathCanvas.enabled == false)
        {
            Pause();
            DefaultMusic.Stop();
            DeathCanvas.enabled = true;
            DeathMusic.Play();
        }
    }

    void FixedUpdate()
    {
        transform.Translate(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, 0);
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
