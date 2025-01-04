using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Enemy")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            Score.score += 1000;
            if (enemy != null)
            {
                enemy.TakeDamage(50);
            }
            Destroy(gameObject);
        }
        if (hitInfo.tag == "Enemy2")
        {
            Enemy2 enemy2 = hitInfo.GetComponent<Enemy2>();
            Score.score += 1000;
            if (enemy2 != null)
            {
                enemy2.TakeDamage(50);
            }
            Destroy(gameObject);
        }
        if (hitInfo.tag == "DeleteBullet")
        {
            Destroy(gameObject);
            Score.score -= 65;
        }
    }
}
