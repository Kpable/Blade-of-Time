using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [HeaderAttribute("Dependencies")]
    public GameManager gameManager;
    public Slider healthSlider;

    [HeaderAttribute("Player Attributes")]
    public float currentHealth;
    public float maxHealth;
    public int experiencePoints;

    void Start () {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

	void Update () {
		
	}

    public void TakeStats(PlayerAttributes stats)
    {
        maxHealth = stats.maxHealth;
        currentHealth = stats.maxHealth;
        experiencePoints = stats.totalExp;
        healthSlider.value = currentHealth;
    }

    public void AlterHealth(float amount)
    {
        if(currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
            healthSlider.value = currentHealth;

        } else if( currentHealth + amount <= 0)
        {
            gameManager.HandleTimerEnd();
        }
        else
        {
            currentHealth += maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colliding with enemy");
        }
    }
}
