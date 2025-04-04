using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class GoUp : MonoBehaviour
{
    FadeInOut fade;
    public Image fadeToBlack;
    public GameObject player;
    public Camera cam;
    public GameObject stairsDown;
    private bool coroutineRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator GoUpStairs()
    {
        coroutineRunning = true;
        fade.FadeOutFalse();
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(stairsDown.transform.position.x - 2, stairsDown.transform.position.y, stairsDown.transform.position.z);
        cam.transform.position = new Vector3(stairsDown.transform.position.x, stairsDown.transform.position.y, cam.transform.position.z);
        fade.FadeOut();
        fade.FadeInFalse();
        coroutineRunning = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (coroutineRunning != true)
            {
                Debug.Log("Go Up");
                StartCoroutine(GoUpStairs());
            }
        }
    }
}

