using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerInsructions : MonoBehaviour
{
    // Reference to the background music GameObject
    public GameObject backgroundMusic;

    public void back()
    {
        // Check if the background music GameObject exists
        if (backgroundMusic != null)
        {
            // Ensure the background music is not destroyed when switching scenes
            Destroy(backgroundMusic);
        }

        SceneManager.LoadScene("StartScene");
    }
}
