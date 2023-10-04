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

    [SerializeField] GameObject Bear;
    [SerializeField] GameObject Deer;
    [SerializeField] GameObject Lizard;
    [SerializeField] GameObject Monkey;
    [SerializeField] GameObject Snake;
    [SerializeField] GameObject Player;

    public HealthManager healthManager;
    private float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        DisableQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        Image[] questions = {BearQuestion, DeerQuestion, LizardQuestion, MonkeyQuestion, SnakeQuestion, RainQuestion, BerriesQuestion};
        DisableQuestions();

        for (int i = 0; i < 7; i++) {
            if (i == ShowQuestion()) {
                questions[i].enabled = true;
            }
        }
    }

    void DisableQuestions() {
        BearQuestion.enabled = false;
        BerriesQuestion.enabled = false;
        RainQuestion.enabled = false;
        DeerQuestion.enabled = false;
        LizardQuestion.enabled = false;
        MonkeyQuestion.enabled = false;
        SnakeQuestion.enabled = false;
    }

    int ShowQuestion() {
        int show = 7;
        float smallestDist = 30;

        if (Vector3.Distance(Bear.transform.position, Player.transform.position) < smallestDist) {
            smallestDist = Vector3.Distance(Bear.transform.position, Player.transform.position);
            show = 0;
        }
        
        if (Vector3.Distance(Deer.transform.position, Player.transform.position) < smallestDist) {
            smallestDist = Vector3.Distance(Deer.transform.position, Player.transform.position);
            show = 1;
        }
        
        if (Vector3.Distance(Lizard.transform.position, Player.transform.position) < smallestDist) {
            smallestDist = Vector3.Distance(Lizard.transform.position, Player.transform.position);
            show = 2;
        }
        
        if (Vector3.Distance(Monkey.transform.position, Player.transform.position) < smallestDist) {
            smallestDist = Vector3.Distance(Monkey.transform.position, Player.transform.position);
            show = 3;
        }
        
        if (Vector3.Distance(Snake.transform.position, Player.transform.position) < smallestDist) {
            smallestDist = Vector3.Distance(Snake.transform.position, Player.transform.position);
            show = 4;
        }

        if (GameObject.FindWithTag("Rain") != null) {
            show = 5;
        }
        
        if (healthManager.health < 30) {
            show = 6;
        }

        return show;
    }

    // // Show berries question when a berry is close to the player
    // float ShowBerriesQuestion() {
    //     var berries = GameObject.FindGameObjectsWithTag("Item");
    //     foreach (var berry in berries) {
    //         distance = Vector3.Distance(berry.transform.position, Player.transform.position);

    //         if (distance <= 200) {
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}
