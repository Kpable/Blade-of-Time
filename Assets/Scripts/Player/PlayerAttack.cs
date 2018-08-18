using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAttack : MonoBehaviour {

    private GameObject weapon;

    private float attackSpeed = 1f;
    private bool canAttack = true;
    private float damage = 1f;
    private float weaponSize = 1f;

    private Sequence swordSwingSequence;
    
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

    public void TakeStats(PlayerAttributes stats)
    {
        attackSpeed = stats.attackSpeed;
        weaponSize = stats.weaponSize;
        damage = stats.damage;

        swordSwingSequence = DOTween.Sequence();
        swordSwingSequence
            .Append(weapon.transform.DOLocalMoveY(0.15f, attackSpeed / 3)).SetEase(Ease.Flash)
            .Join(weapon.transform.DOLocalRotate(Vector3.zero, attackSpeed / 3))
            .Append(weapon.transform.DOLocalMoveY(-0.15f, attackSpeed / 3)).SetEase(Ease.Flash)
            .Join(weapon.transform.DOLocalRotate(new Vector3(0, 0, -179), attackSpeed / 3))
            .Append(weapon.transform.DOLocalMoveY(0f, attackSpeed / 3)).SetEase(Ease.Flash)
            .Join(weapon.transform.DOLocalRotate(new Vector3(0, 0, -90), attackSpeed / 3))
            ;
        swordSwingSequence.Pause();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Debug.Log(name + ": Swingin my sword!");
            canAttack = false;
            //weapon.transform.DOMoveY(weapon.transform.position.x - 0.15f, attackSpeed);
            swordSwingSequence.Play().OnComplete(() => { canAttack = true; swordSwingSequence.Rewind(); });
        }
	}
}
