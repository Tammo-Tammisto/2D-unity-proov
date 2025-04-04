using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class GoDown : MonoBehaviour
{
    FadeInOut fade;
    public Image FadeToBlack;
    public GameObject player;
    public Camera cam;
    public GameObject stairsUp;
    private bool coroutineRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<FadeInOut>();
    }

    public IEnumerator GoDownStairs()
    {
        coroutineRunning = true;
        fade.FadeOutFalse();
        fade.FadeIn();
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(stairsUp.transform.position.x - 2, stairsUp.transform.position.y, stairsUp.transform.position.z);
        cam.transform.position = new Vector3(stairsUp.transform.position.x, stairsUp.transform.position.y, cam.transform.position.z);
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
                Debug.Log("Go Down");
                StartCoroutine(GoDownStairs());
            }

        }
    }
}
