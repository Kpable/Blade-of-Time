using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private GameObject weapon;

    private float attackSpeed = 1f;

    private void Awake()
    {
        weapon = transform.Find("PlayerSprites").Find("PlayerWeapon").gameObject;
        if(weapon)
        {
            //Debug.Log(name + ": Weapon Found");
        }
        else
        {
            //Debug.LogError(name + ": Weapon Not found");
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(name + ": Swingin my sword!");
        }
	}
}
