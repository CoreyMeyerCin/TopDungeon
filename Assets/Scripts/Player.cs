using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mover
{
    //Mover > Fighter contains floats for maxHitpoints, hitPoints, pushRecoverySpeed, and immuneTime
    public static Player instance;
    public Vector3 currentPosition;
    private SpriteRenderer spriteRenderer;

    public double playerDirection;
    public Text collectedText;
    public static int collectedAmount = 0;

    public int level = 1;
    public int gold;

    public HealthService healthService;
    //public Projectile projectilePrefab;//holds the weapons prefab, might be able to do this in a better way to do this in the weapon
    public Dagger projectilePrefab;
    public Weapon weapon;
    public Transform firePoint;

    public int gold;
    public float projectileSpeed;
    private float lastFire;
    public float fireDelay;
    public float attackSpeed = 1f;
    public float cooldown = 1f;

    public float lifespan=1f;//this is used for *projectile range* in Dagger.cs

    protected override void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentPosition = transform.position;
        firePoint = this.transform;
        projectilePrefab = GetComponentInChildren<Weapon>().projectilePrefab;
        weapon = GetComponentInChildren<Weapon>();
        //projectilePrefab = GameManager.instance.player.transform.GetChild(0).GetComponent<Projectile>();
    }

    //instance = this;
    //DontDestroyOnLoad(gameObject);
    //SceneManager.sceneLoaded += LoadState;

    private void Update()
    {
        GetPlayerDirection();
        firePoint = this.transform;
        
        //projectilePrefab = GameManager.instance.player.transform.GetChild(0).gameObject.GetComponent<Projectile>();


    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical"); // same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        UpdateMotor(new Vector3(x, y, 0));
        currentPosition = transform.position;
    }
    public void ProjectileLifespanChange(float lifesp)
    {
        lifespan += lifesp;
    }
    public void ChangeCurrentProjectile(Dagger proj,Weapon weap)
    {
        weapon = weap;
        projectilePrefab = proj;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().weaponDamage = weap.weaponDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().weaponDamage = weap.weaponDamage;
    }
    public void ChangeCurrentWeapon(Weapon weap)
    {
        weapon = weap;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().weaponDamage = weap.weaponDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().weaponDamage = weap.weaponDamage;
    }
    public void CurrentHitPointChange(float currHitPoint)
    {
        hitpoints += currHitPoint;
    }
    public void MaxHitPointsChange(float maxHitPoint)
    {
        maxHitpoints += maxHitPoint;
        hitpoints += maxHitPoint;
    }
    public void AttackSpeedChange(float attackSpeedMod)
    {
        attackSpeed *= attackSpeedMod;
    }
    public void PushRecoveryChange(float pushRecovery)
    {
        pushRecoverySpeed *= pushRecovery;
    }
    
    public void ChangeCooldown(float cooldownMod)
    {
        cooldown -= cooldownMod;
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite= GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoints++;
        hitpoints = maxHitpoints;
    }

    public void SetLevel(int Level)
    {
        for(int i=0; i<Level; i++)
        {
            OnLevelUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("This has collided");
        if (collision.tag.Equals("Collectable"))
        {

        }
    }


    //Movement
    public void GetPlayerDirection()
    {

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            playerDirection = 0.5;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            playerDirection = 1.5; ;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            playerDirection = 2.5; ;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            playerDirection = 3.5;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerDirection = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerDirection = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerDirection = 2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            playerDirection = 3;
        }

       
        
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite= GameManager.instance.playerSprites[skinId];
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player has collided with something");
    }

    //************************************************
    //ACCESSOR METHODS
    //************************************************
    public int GetLevel()
    {
        return level;
    }

    public void SetLevel(int levelToSet)
    {
        level = levelToSet;
    }

}
