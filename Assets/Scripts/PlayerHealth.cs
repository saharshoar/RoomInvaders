using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //testing player's heath by Space key  
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        AudioController.instance.PlayPlayerHurt();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void AddHealth(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void ExpandHealthBar()
    {
        RectTransform rt = healthBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200, 22.5f);
        rt.anchoredPosition3D = new Vector3(135, -35, 0);
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }
}
