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
    public Image healthBar;
    public float health = 100f;

    [SerializeField] int reduceHealthMultiplier = 1;

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Debug.Log("you died");
            SceneManager.LoadScene("EndScreen");
        }

        if (script.distance < 5 && script.counter % 75 == 0 && script.isAttacking)
        {
            Debug.Log("Take Damage");
            TakeDamage(25);
        }

        // Slowly drain health as time passes
        health -= Time.deltaTime * reduceHealthMultiplier;
        healthBar.fillAmount = health / 100f;

    }

    public void OnHealButtonClick()
    {
        Debug.Log("Heal Button Clicked");
        Heal(5);
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
}