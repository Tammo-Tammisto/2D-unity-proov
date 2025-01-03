using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Player")
        {
            PlayerScript player = hitInfo.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(1);
                Score.score -= 899;
            }
            Destroy(gameObject);
        }
        if (hitInfo.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
