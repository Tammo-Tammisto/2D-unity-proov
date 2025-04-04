using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class WorldSwitch : MonoBehaviour
{
    public static bool isWorld1 = true;
    public static bool test = true;
    private static List<GameObject> world1Objects;
    private static List<GameObject> world2Objects;
    private static bool isInitialized = false;
    public static float fadeSpeed = 1f;
    private static SpriteRenderer playerRenderer;
    private static Light2D playerLight;
    private static Coroutine activeTransitionCoroutine;
    private static WorldSwitch instance;

    public AudioSource switchSound; // Reference to the AudioSource

    public static Color world1PlayerColor = new Color(26f / 255f, 26f / 255f, 26f / 255f, 1f);
    public static Color world2PlayerColor = Color.white;

    private class MovingObject
    {
        public GameObject obj;
        public Vector3 world1Position;
        public Vector3 world2Position;
        public bool isMoving;
    }
    private static List<MovingObject> movingObjects = new List<MovingObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeWorldObjects();
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset initialization and reload objects
        isInitialized = false;
        world1Objects = null;
        world2Objects = null;
        movingObjects.Clear();
        playerRenderer = null;
        playerLight = null;

        InitializeWorldObjects();
    }

    private static void InitializeWorldObjects()
    {
        if (isInitialized) return;

        world1Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("World1"));
        world2Objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("World2"));

        // Find Player & get its components
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerRenderer = player.GetComponent<SpriteRenderer>();
            playerLight = player.GetComponent<Light2D>();
            UpdatePlayerColor(instant: true);
        }

        // Initialize moving objects
        foreach (GameObject obj in world1Objects)
        {
            if (obj != null && obj.GetComponent<MovesBetweenWorlds>() != null)
            {
                movingObjects.Add(new MovingObject()
                {
                    obj = obj,
                    world1Position = obj.transform.position,
                    world2Position = obj.transform.position + new Vector3(0, 2f, 0),
                    isMoving = true
                });
            }
        }

        foreach (GameObject obj in world2Objects)
        {
            if (obj != null && obj.GetComponent<MovesBetweenWorlds>() != null)
            {
                movingObjects.Add(new MovingObject()
                {
                    obj = obj,
                    world1Position = obj.transform.position - new Vector3(0, 2f, 0),
                    world2Position = obj.transform.position,
                    isMoving = true
                });
            }
        }

        SetInitialAlphaStates();
        isInitialized = true;
    }

    private static void UpdatePlayerColor(bool instant = false, float alpha = 1f)
    {
        if (playerRenderer == null) return;

        Color targetColor = isWorld1 ? world1PlayerColor : world2PlayerColor;
        targetColor.a = alpha;

        if (instant)
        {
            playerRenderer.color = targetColor;
            if (playerLight != null)
            {
                playerLight.color = targetColor;
            }
        }
        else
        {
            playerRenderer.color = Color.Lerp(playerRenderer.color, targetColor, fadeSpeed * Time.deltaTime);
            if (playerLight != null)
            {
                playerLight.color = Color.Lerp(playerLight.color, targetColor, fadeSpeed * Time.deltaTime);
            }
        }
    }

    private static void SetInitialAlphaStates()
    {
        foreach (GameObject obj in world1Objects)
        {
            if (obj != null) SetObjectAlpha(obj, isWorld1 ? 1f : 0f);
        }

        foreach (GameObject obj in world2Objects)
        {
            if (obj != null) SetObjectAlpha(obj, isWorld1 ? 0f : 1f);
        }

        if (playerRenderer != null)
        {
            UpdatePlayerColor(instant: true, alpha: isWorld1 ? 1f : 1f);
        }

        if (playerLight != null)
        {
            playerLight.intensity = isWorld1 ? 1f : 0f;
        }
    }

    public static void SwitchWorld()
    {
        if (instance == null)
        {
            Debug.LogError("WorldSwitch instance not found!");
            return;
        }

        // Play the sound
        if (instance.switchSound != null)
        {
            instance.switchSound.Play();
        }

        isWorld1 = !isWorld1;

        if (activeTransitionCoroutine != null)
        {
            instance.StopCoroutine(activeTransitionCoroutine);
        }

        activeTransitionCoroutine = instance.StartCoroutine(TransitionWorlds());

        foreach (GameObject obj in world1Objects)
        {
            obj.SetActive(isWorld1);
        }

        foreach (GameObject obj in world2Objects)
        {
            obj.SetActive(!isWorld1);
        }
    }

    private static IEnumerator TransitionWorlds()
    {
        float transitionProgress = 0f;

        while (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime * fadeSpeed;
            float world1Alpha = isWorld1 ? transitionProgress : 1 - transitionProgress;
            float world2Alpha = isWorld1 ? 1 - transitionProgress : transitionProgress;

            // Fade World1 objects
            foreach (GameObject obj in world1Objects)
            {
                if (obj != null) SetObjectAlpha(obj, world1Alpha);
            }

            // Fade World2 objects
            foreach (GameObject obj in world2Objects)
            {
                if (obj != null) SetObjectAlpha(obj, world2Alpha);
            }

            // Update Player
            if (playerRenderer != null)
            {
                UpdatePlayerColor(alpha: isWorld1 ? world1Alpha : world2Alpha);
            }

            // Update Player Light
            if (playerLight != null)
            {
                playerLight.intensity = isWorld1 ? world1Alpha : world2Alpha;
            }

            // Move objects between worlds
            for (int i = movingObjects.Count - 1; i >= 0; i--)
            {
                if (movingObjects[i].obj == null)
                {
                    movingObjects.RemoveAt(i);
                    continue;
                }

                if (movingObjects[i].isMoving)
                {
                    movingObjects[i].obj.transform.position = Vector3.Lerp(
                        isWorld1 ? movingObjects[i].world2Position : movingObjects[i].world1Position,
                        isWorld1 ? movingObjects[i].world1Position : movingObjects[i].world2Position,
                        transitionProgress
                    );
                }
            }

            yield return null;
        }

        SetInitialAlphaStates();
        activeTransitionCoroutine = null;
    }

    private static void SetObjectAlpha(GameObject obj, float alpha)
    {
        if (obj == null) return;

        // Handle SpriteRenderer
        SpriteRenderer renderer = obj.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }

        // Handle Light2D
        Light2D light = obj.GetComponent<Light2D>();
        if (light != null)
        {
            light.color = new Color(light.color.r, light.color.g, light.color.b, alpha);
        }

        // Handle Collider2D
        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = alpha > 0.5f;
        }
    }
}

public class MovesBetweenWorlds : MonoBehaviour
{
    public Vector3 world2Offset = new Vector3(0, 2f, 0);

    void Start()
    {
        // Optional auto-registration
        // WorldSwitch.RegisterWorldObject(gameObject, true);
    }
}