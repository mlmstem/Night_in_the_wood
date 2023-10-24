using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=hxpUk0qiRGs 

// if timer between 4 and 3:55 show something

public class Timer : MonoBehaviour
{

    public bool isTimerActive = false;
    public float timeLeft;
    public Text TimerText;
    public Image UpdateBG;
    public GameObject helicopter;
    public GameObject player;

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
            if (timeLeft > 0) //change to 0
            {
                timeLeft -= Time.deltaTime;
                updateTimeText(timeLeft);
            }
            else
            {
                float distance = Vector3.Distance(helicopter.transform.position, player.transform.position);
                if (distance < 5)
                {
                    Debug.Log("Congratulations, the rescue team has arrived!");
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

        if (sec >= 55 && sec <= 60 && min < 4) {
            UpdateBG.enabled = true;

            if (min == 3) { 
                TimerText.text = string.Format("Someone has noticed you're missing and has sent for help. You have four minutes until they arrive!");
            } else if (min == 2) {
                TimerText.text = string.Format("The rescue team are on their way. You have three minutes until they arrive!");
            } else if (min == 1) {
                TimerText.text = string.Format("The rescue team have landed in the forest. You have two minutes until they arrive!");
            } else if (min == 0) {
                TimerText.text = string.Format("The rescue team have located you. You have one minute until they arrive! Press m to find the meeting point in your map");
            }

        } else if (sec >= 25 && sec <= 30 && min < 0) {
            TimerText.text = string.Format("You have 30 seconds until the rescue team arrive arrive!");
        } else {
            UpdateBG.enabled = false;
            TimerText.text = string.Format("");
        }

    }
}
