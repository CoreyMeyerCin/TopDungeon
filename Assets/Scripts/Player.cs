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
    public Projectile projectilePrefab;
    public Weapon weapon;
    public Transform firePoint;

    private float lastFire;

    public float attackSpeed = 1f;
    public float playerDamage;
    public float lifesteal;
    public float critChance = 5f;
    public float critMultiplier = 1.05f;
    public float cooldown = 1f;

    public int dropChanceModifier = 0;

    public float dashTime = 1f;// this is how long it takes to complete the full dash
    public float currentDashTime = 0f;
    public float endDashTime=0f;
    private bool isDashing = false;
    private Vector3 dashStart, dashEnd;


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

    private void OnEnable()
    {
        Actions.OnLevelUp += OnLevelUp;
    }

    private void OnDisable()
    {
        Actions.OnLevelUp -= OnLevelUp;
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
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //Debug.LogWarning($"UpdateCheck: Time:{Time.time}, endDashTime: {endDashTime}");
            if (isDashing == false && Time.time >= endDashTime)
            {
                //Dash starts
                isDashing = true;
                currentDashTime = Time.time;
                dashStart = currentPosition;
                SetDashLocationGoal(playerDirection);
                Dash(dashEnd);
            }
        }
    }

    public void SetDashLocationGoal(double playerDir)
    {
        switch (playerDir)
        {
            case 0:
                dashEnd = new Vector3(currentPosition.x + (speed/5), currentPosition.y,currentPosition.z);
                return;
            case 0.5:
                dashEnd = new Vector3(currentPosition.x + ((3*speed/4)/5), currentPosition.y-((3*speed/4)/5), currentPosition.z);
                return;
            case 1:
                dashEnd = new Vector3(currentPosition.x, currentPosition.y - ((3 * speed / 4) / 5), currentPosition.z);
                return;
            case 1.5:
                dashEnd = new Vector3(currentPosition.x - ((3 * speed / 4) / 5), currentPosition.y - ((3 * speed / 4) / 5), currentPosition.z);
                return;
            case 2:
                dashEnd = new Vector3(currentPosition.x - (speed/10), currentPosition.y, currentPosition.z);
                return;
            case 2.5:
                dashEnd = new Vector3(currentPosition.x - ((3*speed/4)/5), currentPosition.y + ((3 * speed / 4) / 5), currentPosition.z);
                return;
            case 3:
                dashEnd = new Vector3(currentPosition.x, currentPosition.y + speed/5, currentPosition.z);
                return;
            case 3.5:
                dashEnd = new Vector3(currentPosition.x + ((3 * speed / 4) / 5), currentPosition.y + ((3 * speed / 4) / 10), currentPosition.z);
                return;
        }
    }
    public void Dash(Vector3 dashEnding)
    {
        if (isDashing)
        {
            
            endDashTime = Time.time + dashTime;//we add the current time to 0f to start the dash sequence+
            //Debug.LogWarning($"Dashing is happening: Time:{Time.time}, endDashTime: {endDashTime}");
            float perc = Mathf.Clamp01(currentDashTime / dashTime);

            transform.position = Vector3.Lerp(dashStart, dashEnd, perc);
            if(currentDashTime >= dashTime)
            {
                //dash finished
                isDashing = false;
                transform.position = dashEnd;
            }
        }
        
    }
    public void LifestealChange(float lifest)
    {
        lifesteal += lifest;
    }

    public void PlayerDamageChange(float playerDmg)
    {
        playerDamage += playerDmg;
    }

    public void CritMultiplierChange(float CritDmg)
    {
        critMultiplier += CritDmg;
    }

    public void CritChanceChange(float critCh)
    {
        critChance += critCh;
    }

    public void ChangeCurrentProjectile(Projectile proj, Weapon weap)
    {
        weapon = weap;
        projectilePrefab = proj;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().weaponBaseDamage = weap.weaponBaseDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().weaponBaseDamage = weap.weaponBaseDamage;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Animator>().enabled = false;
    }

    public void ChangeCurrentWeapon(Weapon weap)
    {
        weapon = weap;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().weaponBaseDamage = weap.weaponBaseDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().weaponBaseDamage = weap.weaponBaseDamage;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Animator>().enabled = true;
    }

    public void CurrentHitPointChange(float currHitPoint)
    {
        if (currHitPoint + hitpoints > maxHitpoints)
        {
            hitpoints = maxHitpoints;
        }
        else
        {
            hitpoints += currHitPoint;
        }
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
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        level++;
        MaxHitPointsChange(5f);
        PlayerDamageChange(4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //UnityEngine.Debug.Log($"Player has collided");
        //if (collision.tag.Equals("Portal"))
        //{
        //    UnityEngine.Debug.Log($"{collision}");
        //}
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
            playerDirection = 1.5;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            playerDirection = 2.5;
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