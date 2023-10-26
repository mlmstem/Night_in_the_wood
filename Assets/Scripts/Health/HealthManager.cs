using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Modified code from unity tutorial: https://www.youtube.com/watch?v=0tDPxNB2JNs 
//public AIChase script;
public class HealthManager : MonoBehaviour
{
    public AIChase script;
    public Monkey_chase script_monkey;
    public Deer_chase script_deer;

    public Lizard_snake_chase script_snake;

    public Lizard_snake_chase script_lizard;

    public Image healthBar;
    public float health = 100f;
    private float reduceHealthMultiplier = 0.6f;
    private bool isInTriggerZone = false;

     public AudioSource damageSound;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) {
            return;
        }

        if (health <= 0)
        {
           SceneManager.LoadScene("failscreen");
        }

        // Bear damage
        if (script.distance < 6 && script.counter % 30 == 0 && script.isAttacking)
        {
            TakeDamage(25);
            damageSound.Play();
        }
        // Monkey damage
        else if (script_monkey.distance < 6 && script_monkey.counter % 30 == 0 && script_monkey.isAttacking)
        {
            TakeDamage(15);
            damageSound.Play();
        }
        // Deer damage
        else if (script_deer.distance < 6 && script_deer.counter % 30 == 0 && script_deer.isAttacking)
        {
            TakeDamage(15);
            damageSound.Play();
        }
        // Snake damage
        else if (script_snake.distance < 6 && script_snake.counter % 30 == 0 && script_snake.isAttacking)
        {
            TakeDamage(20);
            damageSound.Play();
        }
        // Lizard damage
        else if (script_lizard.distance < 6 && script_lizard.counter % 30 == 0 && script_lizard.isAttacking)
        {
            TakeDamage(10);
            damageSound.Play();
        }


        if (isInTriggerZone && GameObject.Find("Rain(Clone)") != null)
        {
            // Health not lost if hiding in shelter during rain
        }
        else
        {
            // Slowly drain health as time passes
            if (GameObject.Find("Rain(Clone)") != null)
            {
                // Increase health lost in the rain
                health -= Time.deltaTime * reduceHealthMultiplier * 4;
            }
            else
            {
                health -= Time.deltaTime * reduceHealthMultiplier;
            }

            healthBar.fillAmount = health / 100f;
        }

    }

    public void OnHealButtonClick()
    {
        Debug.Log("Heal Button Clicked");
        Heal(10);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;
    }

    private void Heal(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, 100);
        healthBar.fillAmount = health / 100f;
    }

    private void OnTriggerEnter(Collider other)
    {

        // Check if the player entered the shelter
        if (other.name == "ShelterTrigger")
        {
            // Player entered the shelter, stop reducing health
            isInTriggerZone = true;
        }

    }

    // OnTriggerExit is called when the player exits the trigger zone
    private void OnTriggerExit(Collider other)
    {

        // Check if the player exited the shelter
        if (other.name == "ShelterTrigger")
        {
            // Player exited the shelter, resume reducing health
            isInTriggerZone = false;
        }

    }
}