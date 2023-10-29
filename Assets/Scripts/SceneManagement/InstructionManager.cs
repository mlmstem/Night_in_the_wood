using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] GameObject instructions;

    // Reference to the background music GameObject

    void Start()
    {
        instructions.SetActive(false);

        // Ensure the background music is not destroyed when loading scenes
    }

    public void Instructions()
    {
        instructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructions.SetActive(false);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    // This method will be called when switching scenes
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the current scene
        if (scene.name == "MainScene")
        {
            // If it's the MainScene, destroy the background music
        }
    }
}
