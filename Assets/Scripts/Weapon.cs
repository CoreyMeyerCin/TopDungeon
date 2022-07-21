using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : Collidable
{
	public Sprite sprite;
	public float baseDamage;
	public float additionalDamage;
	public float knockBack;

	private Animator anim;
	private float lastSwingTime;
	private bool attackAvailable;
	public Projectile projectilePrefab;
	public SwingPrefab swingPrefab;
	public Vector3 holdPosition;
	public Vector3 currentHoldPosition;

	public WeaponType weaponType = WeaponType.Melee; //TODO: remove these default enum values
	public DamageType damageType = DamageType.Slashing;
	public WeaponAnimTypeAttack animAttackType = WeaponAnimTypeAttack.SwingSword;
	public WeaponAnimTypeHold animHoldType = WeaponAnimTypeHold.IsSword;

	private void Awake()
	{
	}

	protected override void Start()
	{
		base.Start();
		//spriteRenderer = GetComponent<SpriteRenderer>(); // this will update our weapon look when we load 'this' in
		anim = GetComponent<Animator>();
		transform.localPosition = holdPosition;
	}

	protected override void Update()
	{
		base.Update();
		//this.transform.parent = GameManager.instance.player.transform;
		//this.transform.localPosition = new Vector2(0.096f, -0.011f);
		
		currentHoldPosition = new Vector3(Player.instance.currentPosition.x + holdPosition.x, Player.instance.currentPosition.y + holdPosition.y, 0);
		transform.position = currentHoldPosition;

        if ((Time.time - lastSwingTime) > (Player.instance.stats.cooldown / Player.instance.stats.attackSpeed))
        {
			attackAvailable = true;
		}

		if (Input.GetKeyDown(KeyCode.Space) && attackAvailable)
		{
			//Debug.LogWarning("Hit 1");
			Attack();
		}
	}

	public int CalculateDamage()
    {
		var damage = baseDamage + Player.instance.stats.combinedDamage;

		if(Random.Range(0,100) <= Player.instance.stats.critChance)
        {
			damage *= Player.instance.stats.critMultiplier;
        }

		damage += additionalDamage;

		return Mathf.RoundToInt(damage);
    }
	
	private void Attack()
	{
		PlayerAnimator.SetAttackAnimation();
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
		if (coll.CompareTag("Enemy"))
		{
			Damage dmg = new Damage()
			{
				damageAmount = CalculateDamage(),
				origin = transform.position,
				knockback = knockBack
			};
			coll.SendMessage("ReceiveDamage", dmg); // send the damage over to the enemy with ReceiveDamage()
		}

	}

	private void Shoot()
    {
		Instantiate(Player.instance.transform.GetChild(0).GetComponent<Weapon>().projectilePrefab, currentHoldPosition, GameManager.instance.mousePosition.transform.rotation);
		attackAvailable = false;
		lastSwingTime = Time.time;
	}

	private void Swing()
	{
		//Instantiate(Player.instance.transform.GetChild(0).GetComponent<Weapon>().swingPrefab, currentHoldPosition, GameManager.instance.mousePosition.transform.rotation);
		//NEED TO MAKE SWINGING LOGIC NOW
		attackAvailable = true;
		lastSwingTime = Time.time;
	}

	private void Cast()
	{
		//cast magic
	}

	public WeaponAnimTypeHold GetWeaponType()
	{
		return animHoldType;
	}
}

public enum WeaponAnimTypeAttack
    {
		SwingSword,
		DaggerThrown,
		ShootBow
    }
	public enum WeaponAnimTypeHold
    {
		IsSword,
		IsDagger,
		IsBow,
		IsStaff,
		IsFist,
		IsMagic
    }

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

}
