using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Mover
{
    //Mover > Fighter contains floats for maxHitpoints, hitPoints, pushRecoverySpeed, and immuneTime
    public static Player instance; 
    private SpriteRenderer spriteRenderer;
    public double playerDirection;
    public Text collectedText;
    public static int collectedAmount = 0;

    public HealthService healthService;
    public GameObject projectilePrefab;//holds the weapons prefab, might be able to do this in a better way to do this in the weapon
    public Weapon weapon;
    public Transform firePoint;

    public float projectileSpeed;
    private float lastFire;
    public float fireDelay;
    public float attackSpeed = 1f;
    public float cooldown = 1f;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        firePoint.position = transform.position;
    }

    //instance = this;
    //DontDestroyOnLoad(gameObject);
    //SceneManager.sceneLoaded += LoadState;

    private void Update()
    {
        GetPlayerDirection();
        
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical"); // same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        UpdateMotor(new Vector3(x, y, 0));
    }

    public void ChangeCurrentWeapon(Weapon weap)
    {
        weapon = weap;
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
}

    
