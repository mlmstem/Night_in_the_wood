
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject backgroundMusic;

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
                if (obj != backgroundMusic)
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
        DontDestroyOnLoad(backgroundMusic);
    }

    public void quit()
    {
        Application.Quit();
    }
}