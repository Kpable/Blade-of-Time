using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [HeaderAttribute("Enemy Attributes")]
    public float currentHealth;
    public float maxHealth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AlterHealth(float amount)
    {
        if (currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth + amount <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            currentHealth += maxHealth;
        }
    }
}
