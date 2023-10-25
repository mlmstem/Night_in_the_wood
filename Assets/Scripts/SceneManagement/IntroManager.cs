using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] GameObject Intro;

    // Start is called before the first frame update
    void Start()
    {
        UnlockCursor();
        Time.timeScale = 0f;
        Intro.SetActive(true);
    }

    public void GotIt() {
        Intro.SetActive(false);
        Time.timeScale = 1f;
        LockCursor();
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
