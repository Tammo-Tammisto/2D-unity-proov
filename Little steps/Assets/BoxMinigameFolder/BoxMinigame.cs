using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxMinigame : MonoBehaviour
{
    public UnityEngine.UI.Image a1;
    public UnityEngine.UI.Image a2;
    public UnityEngine.UI.Image a3;
    public UnityEngine.UI.Image a4;

    public UnityEngine.UI.Image itemImage1;
    public UnityEngine.UI.Image itemImage2;
    public UnityEngine.UI.Image itemImage3;
    public UnityEngine.UI.Image itemImage4;

    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private UnityEngine.UI.Image[] imageSlots;
    private string[] randomInputs;
    private int currentInputIndex;
    private UnityEngine.UI.Image[] itemImages;

    // Start is called before the first frame update
    void Start()
    {
        imageSlots = new UnityEngine.UI.Image[] { a1, a2, a3, a4 };
        foreach (var image in imageSlots)
        {
            image.enabled = false;
        }
        a1.enabled = true;
        itemImages = new UnityEngine.UI.Image[] { itemImage4, itemImage3, itemImage2, itemImage1 };
        int[] randomNumbers = new int[4];
        randomInputs = new string[4];
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            randomNumbers[i] = Random.Range(1, 5);
            if (randomNumbers[i] == 1)
            {
                randomInputs[i] = "up";
            }
            else if (randomNumbers[i] == 2)
            {
                randomInputs[i] = "down";
            }
            else if (randomNumbers[i] == 3)
            {
                randomInputs[i] = "left";
            }
            else if (randomNumbers[i] == 4)
            {
                randomInputs[i] = "right";
            }
        }
        for (int i = 0; i < randomInputs.Length; i++)
        {
            Debug.Log(randomInputs[i]);
            if (randomInputs[i] == "up")
            {
                imageSlots[i].sprite = upSprite;
            }
            else if (randomInputs[i] == "down")
            {
                imageSlots[i].sprite = downSprite;
            }
            else if (randomInputs[i] == "left")
            {
                imageSlots[i].sprite = leftSprite;
            }
            else if (randomInputs[i] == "right")
            {
                imageSlots[i].sprite = rightSprite;
            }
        }
    }

    void Update()
    {
        if (currentInputIndex >= randomInputs.Length)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("SampleScene");
            }
            return;
        }

        // Check for swipe input
        if (GridViewController.LastDraggedDirection != null)
        {
            string swipeDirection = GridViewController.LastDraggedDirection.ToString().ToLower();
            if (randomInputs[currentInputIndex] == swipeDirection)
            {
                imageSlots[currentInputIndex].enabled = false;
                if(currentInputIndex < randomInputs.Length-1){
                    imageSlots[currentInputIndex+1].enabled = true;
                }

                Vector3 direction = GetFlyDirection(randomInputs[currentInputIndex]);
                StartCoroutine(FlyAway(itemImages[currentInputIndex], direction));

                currentInputIndex++;
            }
            else
            {
                Debug.Log("Wrong input!");
            }

            // Reset swipe direction after processing
            GridViewController.ResetDragDirection();
        }
    }

    private Vector3 GetFlyDirection(string input)
    {
        switch (input)
        {
            case "up": return Vector3.up;
            case "down": return Vector3.down;
            case "left": return Vector3.left;
            case "right": return Vector3.right;
            default: return Vector3.zero;
        }
    }

    private IEnumerator FlyAway(UnityEngine.UI.Image itemImage, Vector3 direction)
    {
        float traveled = 0f;

        while (traveled < 1000)
        {
            float step = 700 * Time.deltaTime;
            traveled += step;
            itemImage.transform.position += direction * step;
            yield return null;
        }
    }
}
