using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;


public enum WeaponType
{
	Melee,
	Ranged,
	Magic
}
public enum DamageType
{
	Slashing,
	Bludgeoning,
	Piercing,
	Fire,
	Ice,
	Lightning,
	Force
}
public class Weapon : Collidable
{
	//Damage struct
	public int[] damage = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //amount of damage each weapon with upgrade does
	public float[] knockbackDistance = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f, 3.8f }; //how far you push enemy back for each rank


	//Upgrade
	public int weaponLevel = 0; //the current level of the weapon, later this will be used to determine what damage point and pushForce equal through logic
	public SpriteRenderer spriteRenderer; //this is to change the look of our weapon when we upgrade

	//Swing
	private Animator anim; //reference to the Animator
	private float cooldown = 0.5f; //how fast can we swing again
	private float lastSwing; //timer on when our last swing was

	public WeaponType weaponType = WeaponType.Melee;
	public DamageType damageType = DamageType.Slashing;// (maybe add fire, ice, force etc)

	public Transform firePoint;
	public GameObject daggerPrefab;

	private void Awake()
	{
		//spriteRenderer = GetComponent<SpriteRenderer>();
		firePoint.position = this.transform.position;
	}
	protected override void Start()
	{
		base.Start();
		spriteRenderer = GetComponent<SpriteRenderer>(); // this will update our weapon look when we load 'this' in
		anim = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Attack();
		}

	}

	private void Attack()
	{
		switch (weaponType)
		{
			case WeaponType.Melee:
				Swing();
				break;
			case WeaponType.Ranged:
				Shoot();
				break;
			case WeaponType.Magic:
				Cast();
				break;
				
		}
	}

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.tag.Equals("Fighter") && !coll.name.Equals("Player"))
		{
			Damage dmg = new Damage() //send Damage object to fighter we've hit
			{
				damageAmount = damage[weaponLevel],
				origin = transform.position,
				pushForce = knockbackDistance[weaponLevel]
			};

			coll.SendMessage("ReceiveDamage", dmg); // send the damage over to the enemy using ReceiveDamage()
		}

	}
	private void Shoot()
    {
		Instantiate(daggerPrefab,firePoint.position, firePoint.rotation);
    }

	private void Swing()
	{
		anim.SetTrigger("Swing"); //this set 'Swing' in our Animator when we call this function, using the SpaceKey(Update() holds the call to this)
	}

	private void Cast()
	{
		//cast magic
	}

	public void UpgradeWeapon()
	{
		weaponLevel++;
		spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];

		//Change stats
	}

	public void SetWeaponLevel(int level)
	{
		weaponLevel = level;
		this.spriteRenderer.sprite = GameManager.instance.weaponSprite[level];
	}

	public enum WeaponType
	{
		Melee,
		Ranged,
		Magic
	}

}
