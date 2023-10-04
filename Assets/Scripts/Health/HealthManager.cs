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
    public Image healthBar;
    public float health = 100f;
    [SerializeField] int reduceHealthMultiplier = 1;
    private bool isInTriggerZone = false;

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            SceneManager.LoadScene("FailScreen");
        }

        if ((script.distance < 5 && script.counter % 75 == 0 && script.isAttacking) || (script_monkey.distance < 5 && script_monkey.counter % 75 == 0 && script_monkey.isAttacking) || (script_deer.distance < 5 && script_deer.counter % 75 == 0 && script_deer.isAttacking))
        {
            Debug.Log("Take Damage");
            TakeDamage(25);
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
                health -= Time.deltaTime * reduceHealthMultiplier * 3;
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
            Debug.Log("inzone");
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
            Debug.Log("outzone");
        }

    }
}