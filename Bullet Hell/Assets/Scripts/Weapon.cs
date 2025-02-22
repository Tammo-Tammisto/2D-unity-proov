using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Enemy;
    public Transform firePoint;
    public GameObject laserPrefab;
    public AudioSource laserSound;
    public float fireRate = 10f; // Bullets per second
    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!Enemy)
        {
            DestroyAllLasers();
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        laserSound.Play();
        Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
    }

    void DestroyAllLasers()
    {
        List<GameObject> allLasers = new List<GameObject>();
        allLasers.AddRange(GameObject.FindGameObjectsWithTag("Laser"));
        allLasers.AddRange(GameObject.FindGameObjectsWithTag("EnemyLaser"));
        
        foreach (GameObject laser in allLasers)
        {
            Destroy(laser);
        }
    }
}
