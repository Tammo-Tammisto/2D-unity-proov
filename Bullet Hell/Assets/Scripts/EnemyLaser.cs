using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if(hitInfo.tag == "Player")
        {
            PlayerScript.health -= 25;
            Score.score -= 899;
            Destroy(gameObject);
        }
        if(hitInfo.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
