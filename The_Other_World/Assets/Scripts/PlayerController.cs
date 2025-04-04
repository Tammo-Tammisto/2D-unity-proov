using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public TMP_Text TutorialText;
    public GameObject Player;
    private static bool firstLoad = true;
    public Dialogue dialogue;
    public GameObject Door;

    private bool canInteract = false;
    private bool canInteractWithBluePuzzle = false;
    private bool canInteractWithDifferencePuzzle = false;
    public static bool isDoorUnlocked = false;
    public static bool diedToLaser = false;
    public static bool CanEnterDoor = false;
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1.5f;

    // Dictionary to track hasShownDialogue for each scene
    private static Dictionary<string, bool> sceneDialogueShown = new Dictionary<string, bool>();

    void Start()
    {
        if (firstLoad)
        {
            PlayerPrefs.SetFloat("X", 0f);
            PlayerPrefs.SetFloat("Y", -1.4f);
            firstLoad = false;
        }
        Player.transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        Camera.main.transform.position = new Vector3(PlayerPrefs.GetFloat("camX"), PlayerPrefs.GetFloat("camY"), -10);
        PlayerPrefs.SetFloat("X", -6.2f);
        PlayerPrefs.SetFloat("Y", -1.4f);
        PlayerPrefs.SetFloat("Z", 0);
        PlayerPrefs.SetFloat("camX", 0);
        PlayerPrefs.SetFloat("camY", 0);

        if (isDoorUnlocked)
        {
            Debug.Log("Door is unlocked!");
            Door.SetActive(true);
        }
        else
        {
            Debug.Log("Door is locked!");
            Door.SetActive(false);
        }

        if (diedToLaser)
        {
            diedToLaser = false;
            StartCoroutine(FadeIn());
        }
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.R))
        {
            WorldSwitch.SwitchWorld();

            // Check if dialogue has already been shown for the current scene
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                Debug.Log("Dialogue reference is not null. Starting fade-in.");
                dialogue.StartFadeIn("Hello, World!");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }

        if (currentScene == "Game")
        {
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                dialogue.StartFadeIn("This place... it's cold. Silent. But something feels... off.");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }
        else if (currentScene == "Level 2")
        {
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                dialogue.StartFadeIn("Data anomaly detected. Two realities? I must understand this.");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }
        else if (currentScene == "Level 3")
        {
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                dialogue.StartFadeIn("Lost Signals: The Factory’s Forgotten Code");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }
        else if (currentScene == "FinalRoom")
        {
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                dialogue.StartFadeIn("Is This The End?");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }
        else if (currentScene == "FindTheDifference")
        {
            if (dialogue != null && !HasDialogueBeenShown(currentScene))
            {
                dialogue.StartFadeIn("There's something off about this place. I need to find the differences.");
                MarkDialogueAsShown(currentScene); // Mark dialogue as shown for the current scene
            }
            else if (dialogue == null)
            {
                Debug.LogError("Dialogue reference is null. Ensure it is assigned in the Inspector.");
            }
        }


        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetFloat("X", Player.transform.position.x);
            PlayerPrefs.SetFloat("Y", Player.transform.position.y);
            PlayerPrefs.SetFloat("Z", Player.transform.position.z);
            PlayerPrefs.SetFloat("camX", Camera.main.transform.position.x);
            PlayerPrefs.SetFloat("camY", Camera.main.transform.position.y);
            FindObjectOfType<FadeInOut>().SwitchScene("NumpadScene");
        }

        if (isDoorUnlocked && CanEnterDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                FindObjectOfType<FadeInOut>().SwitchScene("Level 2");
            }
            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                isDoorUnlocked = false; // Reset the door state
                CanEnterDoor = false; // Reset the door state
                FindObjectOfType<FadeInOut>().SwitchScene("Level 3");
            }
            else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                FindObjectOfType<FadeInOut>().SwitchScene("FindTheDifference");
            }
            else if (SceneManager.GetActiveScene().name == "FindTheDifference")
            {
                FindObjectOfType<FadeInOut>().SwitchScene("FinalRoom");
            }
            else if (SceneManager.GetActiveScene().name == "FinalRoom")
            {
                FindObjectOfType<FadeInOut>().SwitchScene("Ending");
            }
        }

        if (canInteractWithBluePuzzle && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetFloat("X", Player.transform.position.x);
            PlayerPrefs.SetFloat("Y", Player.transform.position.y);
            PlayerPrefs.SetFloat("Z", Player.transform.position.z);
            PlayerPrefs.SetFloat("camX", Camera.main.transform.position.x);
            PlayerPrefs.SetFloat("camY", Camera.main.transform.position.y);
            FindObjectOfType<FadeInOut>().SwitchScene("BluePuzzleScene");
        }

        if (canInteractWithDifferencePuzzle && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetFloat("X", Player.transform.position.x);
            PlayerPrefs.SetFloat("Y", Player.transform.position.y);
            PlayerPrefs.SetFloat("Z", Player.transform.position.z);
            PlayerPrefs.SetFloat("camX", Camera.main.transform.position.x);
            PlayerPrefs.SetFloat("camY", Camera.main.transform.position.y);
            FindObjectOfType<FadeInOut>().SwitchScene("SwitchingGame");
        }
    }

    private bool HasDialogueBeenShown(string sceneName)
    {
        // Check if the dialogue has been shown for the given scene
        return sceneDialogueShown.ContainsKey(sceneName) && sceneDialogueShown[sceneName];
    }

    private void MarkDialogueAsShown(string sceneName)
    {
        // Mark the dialogue as shown for the given scene
        if (sceneDialogueShown.ContainsKey(sceneName))
        {
            sceneDialogueShown[sceneName] = true;
        }
        else
        {
            sceneDialogueShown.Add(sceneName, true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Numpad"))
        {
            TutorialText.text = "press E to interact";
            canInteract = true; // ✅ Set interaction flag
        }
        if (collision.gameObject.CompareTag("Door") && isDoorUnlocked)
        {
            CanEnterDoor = true;
        }
        if (collision.gameObject.CompareTag("BluePuzzleTrigger"))
        {
            TutorialText.text = "Press E to interact";
            Debug.Log("Blue Puzzle Triggered!");
            canInteractWithBluePuzzle = true; // ✅ Set interaction flag
        }
        if (collision.gameObject.CompareTag("DangerLaser"))
        {
            Debug.Log("Player hit by laser!");

            diedToLaser = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        if (collision.gameObject.CompareTag("DifferencePuzzleArea"))
        {
            TutorialText.text = "Press E to interact";
            canInteractWithDifferencePuzzle = true; // ✅ Set interaction flag
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Numpad"))
        {
            TutorialText.text = "";
            canInteract = false; // ✅ Remove interaction flag
        }
        if (collision.gameObject.CompareTag("Door") && isDoorUnlocked)
        {
            CanEnterDoor = false;
        }
        if (collision.gameObject.CompareTag("BluePuzzleTrigger"))
        {
            TutorialText.text = "";
            Debug.Log("Blue Puzzle Trigger Exited!");
            canInteractWithBluePuzzle = false; // ✅ Remove interaction flag
        }
        if (collision.gameObject.CompareTag("DifferencePuzzleArea"))
        {
            TutorialText.text = "";
            canInteractWithDifferencePuzzle = false; // ✅ Remove interaction flag
        }
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
}