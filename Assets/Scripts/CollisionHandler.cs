using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    bool isControllable = true;
    bool isCollideable = false;
    float delay = 2f;
    [SerializeField]AudioClip crashSFX;
    [SerializeField]AudioClip sucessSFX;
    [SerializeField] ParticleSystem sucessPartical;
    [SerializeField] ParticleSystem crashPartical;
    
    AudioSource audioSource;
    ParticleSystem particleSystem;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        RespondeToDebugKeys();
    }

    private void RespondeToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame){
            isCollideable = !isCollideable;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (!isControllable || isCollideable) { return; }
        
        switch (collision.gameObject.tag) {
            case ("Frendly"):
                Debug.Log("Tag Frendly = LaunchPad");
                break;
            case ("Fule"):
                Debug.Log("Tag Fule = fule");
                break;
            case ("Finish"):
                StartSucessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    private void StartSucessSequence()
    {
        isControllable = false;
        sucessPartical.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sucessSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
        
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        crashPartical.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        GetComponent<Movement>().enabled = false;
        Invoke("RelodLevel", delay);
            
    }

    void RelodLevel()
    {
        int currentScean = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScean);
    }

    void LoadNextLevel()
    {
        int currentScean = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScean + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;

        }

        SceneManager.LoadScene(nextScene);
    }

}
