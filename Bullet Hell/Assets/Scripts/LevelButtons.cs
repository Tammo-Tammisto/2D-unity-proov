using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    public void RetryLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MainMenu(){
        SceneManager.LoadScene(0);
    }
}
