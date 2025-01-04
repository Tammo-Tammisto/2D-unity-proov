using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float moveSpeed = 2f; // Speed of left-right movement
    public float moveRange = 3f; // Distance the enemy moves from its starting position
    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // The point where bullets are instantiated
    public float fireRate = 1f; // Bullets fired per second
    public int bulletCount = 8; // Number of bullets in a circular shot

    private float fireTimer;
    private Vector3 startPosition;
    private int moveDirection = 1; // 1 for right, -1 for left
    public TMP_Text healthText;
    public Canvas infoCanvas;
    public Canvas winCanvas;
    public TMP_Text totalScore;

    void Start()
    {
        winCanvas.enabled = false;
        startPosition = transform.position;
        fireTimer = 1f / fireRate;
    }

    void Update()
    {
        MoveLeftRight();
        HandleShooting();
    }

    void MoveLeftRight()
    {
        // Move left and right within the specified range
        transform.position += Vector3.right * moveDirection * moveSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            moveDirection *= -1; // Reverse direction
        }
    }

    void HandleShooting()
    {
        // Shoot bullets in a circular pattern based on fireRate
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = 1f / fireRate;
            ShootInCircle();
        }
    }

    void ShootInCircle()
    {
        // Pick a random bullet count between 8 and 16 (inclusive)
        int randomBulletCount = UnityEngine.Random.Range(8, 17); // Upper bound is exclusive, so use 17
        float angleStep = 360f / randomBulletCount;

        for (int i = 0; i < randomBulletCount; i++)
        {
            // Calculate the direction for each bullet
            float angle = i * angleStep;
            Vector2 direction = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            // Spawn the bullet
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * 5f; // Set bullet speed (5 units/sec here)
            }
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = " " + health.ToString() + "/4500";
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Score.score += PlayerScript.health * 1000;
        Time.timeScale = 0;
        infoCanvas.enabled = false;
        winCanvas.enabled = true;
        totalScore.text = "Total Score: " + Score.score.ToString() + "PTS";
        Score.totalScore = Score.score;
    }
}