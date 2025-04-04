using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1.5f;
    private static FadeInOut instance;

    void Awake()
    {
        // Ensure this object persists between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn()); // Start scene with fade-in
    }

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        canvasGroup.alpha = 0;
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeAndSwitchScene(string sceneName)
    {
        yield return StartCoroutine(FadeOut()); // Fade out first

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f); // Give time for scene to load

        // Find new CanvasGroup in the new scene
        canvasGroup = FindObjectOfType<CanvasGroup>();

        yield return StartCoroutine(FadeIn()); // Fade back in
    }

}