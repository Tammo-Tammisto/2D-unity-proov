using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas InfoCanvas;
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas.enabled = true;
        InfoCanvas.enabled = false;        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowInfo()
    {
        MainCanvas.enabled = false;
        InfoCanvas.enabled = true;
    }

    public void ReturnToMain()
    {
        MainCanvas.enabled = true;
        InfoCanvas.enabled = false;
    }
}
