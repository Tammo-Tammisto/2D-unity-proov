using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    public float health = 2000f;
    public float shield = 5000f;
    public float moveSpeed = 2f; // Speed of left-right movement
    public float dashSpeed = 6f; // Speed during dash
    public float dashDuration = 1f; // How long the dash lasts
    public float dashChance = 0.1f; // Chance per second to dash
    public float moveRange = 3f; // Distance the enemy moves from its starting position
    public GameObject bulletPrefab; // The bullet prefab
    public Transform firePoint; // The point where bullets are instantiated
    public Transform firePoint2;
    public Transform firePoint3;
    public float fireRate = 1f; // Full cycle of firing across all fire points per second
    public int bulletCount = 8; // Number of bullets in a circular shot

    private float fireTimer;
    private Vector3 startPosition;
    private int moveDirection = 1; // 1 for right, -1 for left
    public TMP_Text healthText;
    public TMP_Text shieldText; // New text for shield
    public Canvas infoCanvas;
    public Canvas winCanvas;
    public TMP_Text totalScore;

    private int currentFirePointIndex = 0;
    private Transform[] firePoints;
    private float fireInterval;
    private bool isDashing = false;
    private bool canShoot = true; // New flag to control shooting

    void Start()
    {
        winCanvas.enabled = false;
        startPosition = transform.position;
        fireTimer = 0f;

        // Initialize the fire points in an array for easy cycling
        firePoints = new Transform[] { firePoint, firePoint2, firePoint3 };

        // Calculate the time interval between each fire point's turn
        fireInterval = 1f / (fireRate * firePoints.Length);

        // Initialize the shield text
        shieldText.text = " " + shield.ToString() + "/5000";

        // Start the dash check coroutine
        StartCoroutine(CheckForDash());
    }

    void Update()
    {
        MoveLeftRight();
        HandleShooting();
    }

    void MoveLeftRight()
    {
        // Move left and right within the specified range
        float currentSpeed = isDashing ? dashSpeed : moveSpeed;
        transform.position += Vector3.right * moveDirection * currentSpeed * Time.deltaTime;

        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            moveDirection *= -1; // Reverse direction
        }
    }

    void HandleShooting()
    {
        if (!canShoot) return; // Prevent shooting while dashing

        // Handle shooting with time interval between fire points
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            fireTimer = fireInterval; // Reset timer for the next fire point
            ShootInCircle(firePoints[currentFirePointIndex]);

            // Move to the next fire point
            currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Length;
        }
    }

    void ShootInCircle(Transform firePoint)
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

    IEnumerator CheckForDash()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Check once per second
            if (!isDashing && UnityEngine.Random.value <= dashChance)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canShoot = false; // Stop shooting while dashing

        // Make the enemy and all fire points invisible
        SetVisibility(false);

        yield return new WaitForSeconds(dashDuration);

        // Make the enemy and all fire points visible again
        SetVisibility(true);
        canShoot = true; // Resume shooting after dash

        Debug.Log(transform.name + " stopped dashing");
        isDashing = false;
    }

    // Helper method to set visibility of the enemy and its children
    void SetVisibility(bool visible)
    {
        // Get the SpriteRenderer of the enemy
        SpriteRenderer enemySpriteRenderer = GetComponent<SpriteRenderer>();
        if (enemySpriteRenderer != null)
        {
            enemySpriteRenderer.enabled = visible;
        }

        // Get all child SpriteRenderers (including fire points)
        SpriteRenderer[] childSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in childSpriteRenderers)
        {
            sr.enabled = visible;
        }
    }

    public void TakeDamage(float damage)
    {
        if (shield > 0)
        {
            shield -= damage * 2;
            shield = Mathf.Max(shield, 0); // Ensure shield doesn't go below 0
            shieldText.text = " " + shield.ToString() + "/5000";
        }
        else
        {
            health -= damage;
            health = Mathf.Max(health, 0); // Ensure health doesn't go below 0
            healthText.text = " " + health.ToString() + "/2000";
            if (health <= 0)
            {
                Die();
            }
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