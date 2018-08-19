using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [HeaderAttribute("Enemy Attributes")]
    public float currentHealth;
    public float maxHealth;
    public float walkSpeed = 10.0f;
    public float currentSpeed = 0.0f;

    private Pathfinding pathfinding;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
    // Use this for initialization
    void Start () {
        currentSpeed = walkSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (pathfinding.pathToFollow != null && pathfinding.pathToFollow.Count >0) FollowPath();
	}

    private void FollowPath()
    {
        Vector3 currentPosition = transform.position;
        currentPosition = Vector3.MoveTowards(currentPosition,
            pathfinding.pathToFollow[0].WorldPosition + new Vector3(0.5f, 0.5f, 0),
            Time.deltaTime * currentSpeed);

        transform.position = currentPosition;
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
