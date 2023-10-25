using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Code modified from https://www.youtube.com/watch?v=tfzwyNS1LUY

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject instructions;

    void Start() {
        pauseMenu.SetActive(false);
        instructions.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            UnlockCursor();
        }
    }

    public void Pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        UnlockCursor();
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        LockCursor();
    }

    public void Quit() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    public void Instructions() {
        instructions.SetActive(true);
    }

    public void CloseInstructions() {
        instructions.SetActive(false);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
