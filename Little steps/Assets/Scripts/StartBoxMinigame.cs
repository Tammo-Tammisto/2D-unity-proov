using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartBoxMinigame : MonoBehaviour
{
    public TMP_Text ePrompt;
    public GameObject Player;


    void Start()
    {
        ePrompt.enabled = false;
        Player.transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        Camera.main.transform.position = new Vector3(PlayerPrefs.GetFloat("camX"), PlayerPrefs.GetFloat("camY"), -10);
        PlayerPrefs.SetFloat("X", 0);
        PlayerPrefs.SetFloat("Y", 0);
        PlayerPrefs.SetFloat("Z", 0);
        PlayerPrefs.SetFloat("camX", 0);
        PlayerPrefs.SetFloat("camY", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && ePrompt.enabled)
        {
            PlayerPrefs.SetFloat("X", Player.transform.position.x);
            PlayerPrefs.SetFloat("Y", Player.transform.position.y);
            PlayerPrefs.SetFloat("Z", Player.transform.position.z);
            PlayerPrefs.SetFloat("camX", Camera.main.transform.position.x);
            PlayerPrefs.SetFloat("camY", Camera.main.transform.position.y);
            SceneManager.LoadScene("BoxMinigame");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            ePrompt.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            ePrompt.enabled = false;
        }
    }
}
