using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{
    public float rotationSpeed = 2f; // Speed of the rotation
    public float maxRotation = 5f;  // Maximum rotation angle

    void Update()
    {
        float zRotation = Mathf.PingPong(Time.time * rotationSpeed, maxRotation * 2) - maxRotation;
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }
}
