using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Questions : MonoBehaviour
{
    [SerializeField] Image RainQuestion;
    [SerializeField] Image BerriesQuestion;
    [SerializeField] Image BearQuestion;
    [SerializeField] Image DeerQuestion;
    [SerializeField] Image LizardQuestion;
    [SerializeField] Image MonkeyQuestion;
    [SerializeField] Image SnakeQuestion;

    [SerializeField] Image RainAnswer;
    [SerializeField] Image BerriesAnswer;
    [SerializeField] Image BearAnswer;
    [SerializeField] Image DeerAnswer;
    [SerializeField] Image LizardAnswer;
    [SerializeField] Image MonkeyAnswer;
    [SerializeField] Image SnakeAnswer;

    [SerializeField] GameObject Bear;
    [SerializeField] GameObject Deer;
    [SerializeField] GameObject Lizard;
    [SerializeField] GameObject Monkey;
    [SerializeField] GameObject Snake;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Panel;
    [SerializeField] GameObject Button;

    [SerializeField] Monkey_chase MonkeyScript;

    public HealthManager healthManager;
    private float distance;
    private bool[] shown = new bool[7];

    void Start()
    {
        DisableQuestions();
        Panel.SetActive(false);
    }

    void Update()
    {
        if (Panel.activeInHierarchy)
        {
            return;
        }

        Image[] questions = { BearQuestion, DeerQuestion, LizardQuestion, MonkeyQuestion, SnakeQuestion, RainQuestion, BerriesQuestion, BearAnswer, DeerAnswer, LizardAnswer, MonkeyAnswer, SnakeAnswer, RainAnswer, BerriesAnswer };
        DisableQuestions();

        int show = ShowQuestion();
        for (int i = 0; i < 14; i++)
        {
            if (i == show)
            {
                questions[i].enabled = true;
                if (i < 7)
                {
                    Panel.SetActive(true);
                    Time.timeScale = 0f;
                    UnlockCursor();
                }
            }
        }
    }

    void DisableQuestions()
    {
        BearQuestion.enabled = false;
        BerriesQuestion.enabled = false;
        RainQuestion.enabled = false;
        DeerQuestion.enabled = false;
        LizardQuestion.enabled = false;
        MonkeyQuestion.enabled = false;
        SnakeQuestion.enabled = false;
        BearAnswer.enabled = false;
        BerriesAnswer.enabled = false;
        RainAnswer.enabled = false;
        DeerAnswer.enabled = false;
        LizardAnswer.enabled = false;
        MonkeyAnswer.enabled = false;
        SnakeAnswer.enabled = false;
    }

    int ShowQuestion()
    {
        int show = 14;
        float smallestDist = 30;

        if (Vector3.Distance(Bear.transform.position, Player.transform.position) < smallestDist)
        {
            smallestDist = Vector3.Distance(Bear.transform.position, Player.transform.position);
            if (shown[0])
            {
                show = 7;
            }
            else
            {
                show = 0;
            }
        }

        if (Vector3.Distance(Deer.transform.position, Player.transform.position) < smallestDist)
        {
            smallestDist = Vector3.Distance(Deer.transform.position, Player.transform.position);
            if (shown[1])
            {
                show = 8;
            }
            else
            {
                show = 1;
            }
        }

        if (Lizard.activeInHierarchy && Vector3.Distance(Lizard.transform.position, Player.transform.position) < smallestDist)
        {
            smallestDist = Vector3.Distance(Lizard.transform.position, Player.transform.position);
            if (shown[2])
            {
                show = 9;
            }
            else
            {
                show = 2;
            }
        }

        if (MonkeyScript.monkey_eats == false && Vector3.Distance(Monkey.transform.position, Player.transform.position) < smallestDist)
        {
            smallestDist = Vector3.Distance(Monkey.transform.position, Player.transform.position);
            if (shown[3])
            {
                show = 10;
            }
            else
            {
                show = 3;
            }
        }

        if (Snake.activeInHierarchy && Vector3.Distance(Snake.transform.position, Player.transform.position) < smallestDist)
        {
            smallestDist = Vector3.Distance(Snake.transform.position, Player.transform.position);
            if (shown[4])
            {
                show = 11;
            }
            else
            {
                show = 4;
            }
        }

        if (GameObject.FindWithTag("Rain") != null)
        {
            if (shown[5])
            {
                show = 12;
            }
            else
            {
                show = 5;
            }
        }

        if (healthManager.health < 30)
        {
            if (shown[6])
            {
                show = 13;
            }
            else
            {
                show = 6;
            }
        }

        if (show < 7)
        {
            shown[show] = true;
        }

        return show;
    }

    public void Resume()
    {
        Panel.SetActive(false);
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
