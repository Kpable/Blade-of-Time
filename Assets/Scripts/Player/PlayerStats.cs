using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [HeaderAttribute("Dependencies")]
    public GameManager gameManager;

    [HeaderAttribute("Player Attributes")]
    public float currentHealth;
    public float maxHealth;
    public Vector3 startPosition;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            startPosition = transform.position;
            created = true;
        }
    }

    void Start () {
        currentHealth = maxHealth;
        transform.position = startPosition;
	}

	void Update () {
		
	}

    public void AlterHealth(float amount)
    {
        if(currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        } else if( currentHealth + amount <= 0)
        {
            gameManager.HandleTimerEnd();
        }
        else
        {
            currentHealth += maxHealth;
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
