using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : Collidable
{
	//Damage struct
	//public int[] damage = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; //amount of damage each weapon with upgrade does
	//public float[] knockbackDistance = { 2.0f, 2.2f, 2.4f, 2.6f, 2.8f, 3.0f, 3.2f, 3.4f, 3.6f, 3.8f }; //how far you push enemy back for each rank

	private Player player;
	//Upgrade
	//public int weaponLevel = 0; //the current level of the weapon, later this will be used to determine what damage point and pushForce equal through logic
	//public SpriteRenderer spriteRenderer; //this is to change the look of our weapon when we upgrade
	public Sprite sprite;
	public float weaponBaseDamage;
	public float weaponExtraDamage;
	public bool hasExtraDamage;
	public float knockBack;
	//Swing
	private Animator anim; //reference to the Animator
	//public float cooldown = 1f; //how fast can we swing again
	private float lastUse; //timer on when our last swing was
	private bool attackAvailable;
	public Dagger projectilePrefab;



	public WeaponType weaponType = WeaponType.Melee;
	public DamageType damageType = DamageType.Slashing;// (maybe add fire, ice, force etc)


	private void Awake()
	{

		player = gameObject.GetComponentInParent<Player>();
		//spriteRenderer = GetComponent<SpriteRenderer>();
	}
	protected override void Start()
	{
		base.Start();
		//spriteRenderer = GetComponent<SpriteRenderer>(); // this will update our weapon look when we load 'this' in
		anim = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();

		if(Time.time - lastUse > GameManager.instance.player.cooldown)
        {
			attackAvailable = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && attackAvailable)
		{
				Attack();
		}
	}
	public int CalculateDamage(float weaponBaseDamage, float weaponExtraDamage ,float critChance, float critMultiplier, float playerDamage)
    {
		double hybridDamage= weaponBaseDamage+ playerDamage;


		double totalDamage= hybridDamage;
	

		if(Random.Range(0,100) <= critChance)
        {
			totalDamage *= (double)critMultiplier;
        }

        if (hasExtraDamage)
        {
			totalDamage += (double)weaponExtraDamage;
        }
		//if (player.lifesteal > 0)
		//{
		//	player.CurrentHitPointChange((float)totalDamage * player.lifesteal);
		//}

		return (int)totalDamage;

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
		if (coll.tag.Equals("Enemy"))
		{
			Damage dmg = new Damage()
			{
				damageAmount = (int)CalculateDamage(weaponBaseDamage, weaponExtraDamage, player.critChance, player.critMultiplier, player.playerDamage),
				origin = transform.position,
				pushForce = knockBack
			};
			coll.SendMessage("ReceiveDamage", dmg); // send the damage over to the enemy using ReceiveDamage()
		}

	}
	private void Shoot()
    {
		Instantiate(player.projectilePrefab,player.firePoint.position, player.firePoint.rotation);
		attackAvailable = false;
		lastUse = Time.time;
	}

	private void Swing()
	{
		anim.SetTrigger("Swing"); //this set 'Swing' in our Animator when we call this function, using the SpaceKey(Update() holds the call to this)
	}

	private void Cast()
	{
		//cast magic
	}

	//public void UpgradeWeapon()
	//{
	//	weaponLevel++;
	//	spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];

	//	//Change stats
	//}

	//public void SetWeaponLevel(int level)
	//{
	//	weaponLevel = level;
	//	this.spriteRenderer.sprite = GameManager.instance.weaponSprite[level];
	//}

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
