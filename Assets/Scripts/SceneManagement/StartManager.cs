
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject backgroundMusic;

    public void restart()
    {
        SceneManager.LoadScene("MainScene");
        Destroy(backgroundMusic);
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
