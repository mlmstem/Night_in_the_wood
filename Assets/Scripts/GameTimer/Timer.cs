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
    public Image UpdateBG;
    public GameObject helicopter;
    public GameObject player;

    void Start()
    {
        isTimerActive = true;
    }

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
                float distance = Vector3.Distance(helicopter.transform.position, player.transform.position);
                if (distance < 25)
                {
                    isTimerActive = false;
                    SceneManager.LoadScene("EndScreen");
                }
                else
                {
                    isTimerActive = false;
                    SceneManager.LoadScene("FailScreen");
                }
            }
        }
    }

    void updateTimeText(float currentTime)
    {
        currentTime += 1;

        float min = Mathf.FloorToInt(currentTime / 60);
        float sec = Mathf.FloorToInt(currentTime % 60);

        if (sec >= 55 && sec <= 60 && min < 4)
        {

            UpdateBG.enabled = true;

            if (min == 3)
            {
                TimerText.text = string.Format("Someone has noticed you're missing and has sent for help. You have four minutes until they arrive!");
            }
            else if (min == 2)
            {
                TimerText.text = string.Format("The rescue team have left their base. You have three minutes until they arrive!");
            }
            else if (min == 1)
            {
                TimerText.text = string.Format("The rescue team are on their way. You have two minutes until they arrive!");
            }
            else if (min == 0)
            {
                TimerText.text = string.Format("You have one minute until the rescue team arrives! Press m to find the meeting point on your map");
            }

        }
        else if (sec >= 25 && sec <= 30 && min < 0)
        {
            TimerText.text = string.Format("You have 30 seconds until the rescue team arrives! Make sure to get to the meeting point in time");
        }
        else
        {
            UpdateBG.enabled = false;
            TimerText.text = string.Format("");
        }

    }
}
