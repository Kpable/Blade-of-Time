using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [HeaderAttribute("Dependencies")]
    public GameManager gameManager;
    public Slider healthSlider;
    public Slider expSlider;

    [HeaderAttribute("Player Attributes")]
    public float currentHealth;
    public float maxHealth;
    public float experiencePoints;
    public float maxExp;
    public int level = 1;

    void Start () {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void Update () {
		
	}

    public void TakeStats(PlayerAttributes stats)
    {
        maxHealth = stats.maxHealth;
        currentHealth = stats.maxHealth;
        healthSlider.value = currentHealth;
        experiencePoints = stats.currentExp;
        expSlider.value = stats.currentExp;
        expSlider.maxValue = stats.maxExp;
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
            //Debug.Log("Colliding with enemy");
        }
    }

    public void GainExp(float amount)
    {
        experiencePoints += amount;
        if(experiencePoints > maxExp)
        {
            maxExp = gameManager.UpgradePlayer(maxExp);
            experiencePoints = 0;
            expSlider.maxValue = maxExp;
            level++;

        }

        expSlider.value = experiencePoints;
        gameManager.setExp(experiencePoints);
    }
}
