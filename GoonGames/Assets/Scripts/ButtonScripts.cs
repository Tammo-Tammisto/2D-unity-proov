using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public void ConfirmAgeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("GameIntroScene");
    }

    public void SkipStoryButton()
    {
        SceneManager.LoadScene("RedLightEdgeLight");
    }
}
