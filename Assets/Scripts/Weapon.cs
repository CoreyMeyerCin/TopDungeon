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
	public Projectile projectilePrefab;
	public Vector3 holdPosition;
	public Vector3 currentHoldPosition;



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
		transform.localPosition = holdPosition;
	}

	protected override void Update()
	{
		base.Update();
		//this.transform.parent = GameManager.instance.player.transform;
		//this.transform.localPosition = new Vector2(0.096f, -0.011f);
		
		currentHoldPosition = new Vector3(player.currentPosition.x + this.holdPosition.x,
									   player.currentPosition.y + this.holdPosition.y, 0);
		transform.position = currentHoldPosition;
		//Time.time/1 > 1
		if (Time.time - lastUse > Player.instance.stats.cooldown/player.stats.attackSpeed)
        {
			attackAvailable = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && attackAvailable)
		{
			Attack();
		}
	}

	public int CalculateDamage()
    {
		var damage = weaponBaseDamage + Player.instance.playerDamage;

		if(Random.Range(0,100) <= Player.instance.stats.critChance)
        {
			damage *= Player.instance.stats.critMultiplier;
        }

        if (hasExtraDamage)
        {
			damage += weaponExtraDamage;
        }
		//if (player.lifesteal > 0)
		//{
		//	player.CurrentHitPointChange((float)totalDamage * player.lifesteal);
		//}

		return (int)damage;

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
			//Debug.LogWarning("Weapon hit enemy");
			Damage dmg = new Damage()
			{
				damageAmount = CalculateDamage(),
				origin = transform.position,
				pushForce = knockBack
			};
			coll.SendMessage("ReceiveDamage", dmg); // send the damage over to the enemy using ReceiveDamage()
		}

	}

	private void Shoot()
    {
		Instantiate(player.transform.GetChild(0).GetComponent<Weapon>().projectilePrefab,currentHoldPosition, player.firePoint.rotation);
		attackAvailable = false;
		lastUse = Time.time;
	}
	
	private void Swing()
	{
		//NEED TO MAKE SWINGING LOGIC NOW
		//anim.SetTrigger("Swing"); //this set 'Swing' in our Animator when we call this function, using the SpaceKey(Update() holds the call to this)
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
