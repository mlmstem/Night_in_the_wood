using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=hxpUk0qiRGs 

public class Timer : MonoBehaviour
{

    public bool isTimerActive = false;
    public float timeLeft;
    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        isTimerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerActive)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimeText(timeLeft);
            }
            else
            {
                Debug.Log("Congratulations, the rescue team has arrived!");
                isTimerActive = false;
                SceneManager.LoadScene("EndScreen");
            }
        }
    }

    void updateTimeText(float currentTime)
    {
        currentTime += 1;

        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00} : {1:00}", min, sec);
    }
}
