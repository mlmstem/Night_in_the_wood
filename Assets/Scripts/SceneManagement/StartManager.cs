
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private bool musicStarted = false;
    private AudioSource audioSource;

    void Start()
    {
        // Check if there's no background music in the scene with DontDestroyOnLoad
        if (!musicStarted)
        {
            GameObject backgroundMusic = GameObject.Find("BackgroundMusic");

            // If no background music is found, create and set it as a DontDestroyOnLoad object
            if (backgroundMusic == null)
            {
                backgroundMusic = new GameObject("BackgroundMusic");
                DontDestroyOnLoad(backgroundMusic);
                
                // Try to get the AudioSource component from the StartScene
                audioSource = FindObjectOfType<AudioSource>();

                // If an AudioSource is found, configure it and play the music
                if (audioSource)
                {
                    audioSource.gameObject.transform.parent = backgroundMusic.transform;
                }
                else
                {
                    // If no AudioSource is found, log a message
                    Debug.LogWarning("No AudioSource found in StartScene.");
                }

                audioSource.Play();
                musicStarted = true;
            }
        }
    }

    public void restart()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Check if it's the "StartScene"
        if (currentScene.name == "StartScene")
        {
            // Find all GameObjects in the "StartScene"
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

            // Destroy all objects in the "StartScene" except the background music
            foreach (var obj in allObjects)
            {
                if (obj.name != "BackgroundMusic")
                {
                    Destroy(obj);
                }
            }
        }

        SceneManager.LoadScene("MainScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void quit()
    {
        Application.Quit();
    }
}





