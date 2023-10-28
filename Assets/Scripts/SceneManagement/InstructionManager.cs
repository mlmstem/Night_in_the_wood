using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] GameObject instructions;

    void Start() {
        instructions.SetActive(false);
    }

    public void Instructions() {
        instructions.SetActive(true);
    }

    public void CloseInstructions() {
        instructions.SetActive(false);
    }

    public void Quit() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
}
