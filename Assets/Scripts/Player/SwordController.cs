using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour {

    public bool isActive = false;
    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isActive && col.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy Attacked");
            col.gameObject.GetComponent<EnemyController>().AlterHealth(-damage);
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(-2 * (transform.position - col.gameObject.transform.position));
        }
    }
}
