
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void restart()
    {
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
