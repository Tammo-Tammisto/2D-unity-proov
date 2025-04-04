using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Camera cam;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;
    public GameObject floor4;

    // Add lists for objects above each floor
    public List<GameObject> objectsAboveFloor1;
    public List<GameObject> objectsAboveFloor2;
    public List<GameObject> objectsAboveFloor3;
    public List<GameObject> objectsAboveFloor4;

    public String currentFloor;

    private Vector3 targetPosition;

    void Start()
    {
        ResetTransparency();
    }

    public void MoveToFloor1()
    {
        currentFloor = "Floor1";
        SetFloorTransparency(floor1, objectsAboveFloor1);
        targetPosition = new Vector3(floor1.transform.position.x, floor1.transform.position.y, cam.transform.position.z);
    }

    public void MoveToFloor2()
    {
        currentFloor = "Floor2";
        SetFloorTransparency(floor2, objectsAboveFloor2);
        targetPosition = new Vector3(floor2.transform.position.x, floor2.transform.position.y, cam.transform.position.z);
    }

    public void MoveToFloor3()
    {
        currentFloor = "Floor3";
        SetFloorTransparency(floor3, objectsAboveFloor3);
        targetPosition = new Vector3(floor3.transform.position.x, floor3.transform.position.y, cam.transform.position.z);
    }

    public void MoveToFloor4()
    {
        currentFloor = "Floor4";
        SetFloorTransparency(floor4, objectsAboveFloor4);
        targetPosition = new Vector3(floor4.transform.position.x, floor4.transform.position.y, cam.transform.position.z);
    }

    void Update()
    {
        if (cam.transform.position != targetPosition)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPosition, 7f * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Floor1")
        {
            MoveToFloor1();
        }
        else if (other.gameObject.name == "Floor2")
        {
            MoveToFloor2();
        }
        else if (other.gameObject.name == "Floor3")
        {
            MoveToFloor3();
        }
        else if (other.gameObject.name == "Floor4")
        {
            MoveToFloor4();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Floor1" && currentFloor != "Floor1")
        {
            MoveToFloor1();
        }
        else if (other.gameObject.name == "Floor2" && currentFloor != "Floor2")
        {
            MoveToFloor2();
        }
        else if (other.gameObject.name == "Floor3" && currentFloor != "Floor3")
        {
            MoveToFloor3();
        }
        else if (other.gameObject.name == "Floor4" && currentFloor != "Floor4")
        {
            MoveToFloor4();
        }
    }

    void SetFloorTransparency(GameObject activeFloor, List<GameObject> associatedObjects)
    {
        ResetTransparency();

        // Set transparency for the active floor
        SpriteRenderer activeRenderer = activeFloor.GetComponent<SpriteRenderer>();
        if (activeRenderer != null)
        {
            activeRenderer.color = new Color(activeRenderer.color.r, activeRenderer.color.g, activeRenderer.color.b, 1f);
        }

        // Set transparency for associated objects
        foreach (GameObject obj in associatedObjects)
        {
            SetTransparency(obj, 1f); // Fully visible
        }
    }

    void ResetTransparency()
    {
        // Make all floors and associated objects semi-transparent
        SetTransparency(floor1, 0f);
        SetTransparency(floor2, 0f);
        SetTransparency(floor3, 0f);
        SetTransparency(floor4, 0f);

        ResetAssociatedObjectsTransparency(objectsAboveFloor1);
        ResetAssociatedObjectsTransparency(objectsAboveFloor2);
        ResetAssociatedObjectsTransparency(objectsAboveFloor3);
        ResetAssociatedObjectsTransparency(objectsAboveFloor4);
    }

    void ResetAssociatedObjectsTransparency(List<GameObject> objects)
    {
        foreach (GameObject obj in objects)
        {
            SetTransparency(obj, 0f); // Fully transparent
        }
    }

    void SetTransparency(GameObject obj, float alpha)
    {
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        }
    }
}