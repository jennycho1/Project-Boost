using UnityEngine;
using UnityEngine.SceneManagement; // for ReloadLevel

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    // audio assets
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    // particle effects
    [SerializeField] ParticleSystem crashFX;
    [SerializeField] ParticleSystem successFX;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        // Key L: Load next level
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } 
        // Key C: Toggle collision mode
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // while transitioning and collision is off, prevent it from making sounds
        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            // different action cases when the rocket collide with objects
            case "Friendly":
                // safe object to touch
                break;
            case "Finish":
                // success - load next level
                StartSuccessSequence();
                break;
            case "Fuel":
                // TODO: finish fuel behavior
                break;
            default:
                // crash - restart the scene
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        // Play SFX
        audioSource.PlayOneShot(successSFX);
        // Play success FX
        successFX.Play();
        // turn off collision behavior and load next level
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        // Play SFX
        audioSource.PlayOneShot(crashSFX);
        // Play success FX
        crashFX.Play();
        // // turn off collision behavior and load next level
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay); // use invoke to delay seconds
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // hit the last level
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
}
