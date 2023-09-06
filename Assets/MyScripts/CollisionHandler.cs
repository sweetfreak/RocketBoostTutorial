using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource myAudioSource;

    [SerializeField] private float crashDelaySeconds = 1f;
    [SerializeField] private float newLevelDelaySeconds = 2f;

    
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioClip successSfx;

    [SerializeField] private ParticleSystem crashParticle;
    [SerializeField] private ParticleSystem successParticle;
    
    private bool isTransitioning = false;
    private bool collisionDisabled = false;
    
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }
        

        switch (collision.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("friendly");
                    break;
                case "Finish":
                    Debug.Log("finish!");
                    StartCompletedLevelSequence();
                    break;
                // case "Fuel":
                //     Debug.Log("Fuel");
                //     break;
                default:
                    Debug.Log("you blew up!");
                    StartCrashSequence();
                    break;
            
        }
    }

    void StartCompletedLevelSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        myAudioSource.PlayOneShot(successSfx);
        Invoke("LoadNextLevel", newLevelDelaySeconds);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        myAudioSource.PlayOneShot(crashSfx);

        Invoke("ReloadScene", crashDelaySeconds);
    }
    
    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }
}
