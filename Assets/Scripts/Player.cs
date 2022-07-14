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

    public int gold;

    public HealthService healthService;
    public PlayerSpriteService spriteService;
    //public Projectile projectilePrefab;//holds the weapons prefab, might be able to do this in a better way to do this in the weapon
    //public Projectile projectilePrefab;
    public Weapon weapon;
    public Transform firePoint;

    private float lastFire;

    //dashTime was moved to PlayerStats
    //public float dashTime = 1f;// this is how long it takes to complete the full dash
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
        firePoint.position = transform.GetChild(0).GetComponent<Weapon>().holdPosition;
        weapon = GetComponentInChildren<Weapon>();
        weapon.projectilePrefab = GetComponentInChildren<Weapon>().projectilePrefab;        
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
        //firePoint = this.transform;
        //this.GetComponentInChildren<Weapon>().transform.localPosition += new Vector3(0.096f,-0.011f,0);
        
        //projectilePrefab = GameManager.instance.player.transform.GetChild(0).gameObject.GetComponent<Projectile>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // this will give us -1,1,or 0 depending if we are ising a,d, or no input.
        float y = Input.GetAxisRaw("Vertical"); // same thing but with the y axis with w,s, or no input
        //Reset MoveDelta
        UpdateMotor(new Vector3(x, y, 0));
        currentPosition = transform.position;
        
        //firePoint.position = transform.GetChild(0).GetComponent<Weapon>().holdPosition;
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
                dashEnd = new Vector3(currentPosition.x + (stats.speed /5), currentPosition.y,currentPosition.z);
                return;
            case 0.5:
                dashEnd = new Vector3(currentPosition.x + ((3 * stats.speed / 4) / 5), currentPosition.y - ((3 * stats.speed / 4) / 5), currentPosition.z);
                return;
            case 1:
                dashEnd = new Vector3(currentPosition.x, currentPosition.y - ((3 * stats.speed / 4) / 5), currentPosition.z);
                return;
            case 1.5:
                dashEnd = new Vector3(currentPosition.x - ((3 * stats.speed / 4) / 5), currentPosition.y - ((3 * stats.speed / 4) / 5), currentPosition.z);
                return;
            case 2:
                dashEnd = new Vector3(currentPosition.x - (stats.speed / 10), currentPosition.y, currentPosition.z);
                return;
            case 2.5:
                dashEnd = new Vector3(currentPosition.x - ((3 * stats.speed / 4) / 5), currentPosition.y + ((3 * stats.speed / 4) / 5), currentPosition.z);
                return;
            case 3:
                dashEnd = new Vector3(currentPosition.x, currentPosition.y + stats.speed / 5, currentPosition.z);
                return;
            case 3.5:
                dashEnd = new Vector3(currentPosition.x + ((3 * stats.speed / 4) / 5), currentPosition.y + ((3 * stats.speed / 4) / 10), currentPosition.z);
                return;
        }
    }
    public void Dash(Vector3 dashEnding)
    {
        if (isDashing)
        {
            
            endDashTime = Time.time + stats.dashTime;//we add the current time to 0f to start the dash sequence+
            //Debug.LogWarning($"Dashing is happening: Time:{Time.time}, endDashTime: {endDashTime}");
            float perc = Mathf.Clamp01(currentDashTime / stats.dashTime);

            transform.position = Vector3.Lerp(dashStart, dashEnd, perc);
            if(currentDashTime >= stats.dashTime)
            {
                //dash finished
                isDashing = false;
                transform.position = dashEnd;
            }
        }
        
    }

    public void ChangeCurrentProjectile(Projectile proj, Weapon weap)
    {
        weapon = weap;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().projectilePrefab = proj;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        //transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).GetComponent<Weapon>().holdPosition = weap.holdPosition;
    }

    public void ChangeCurrentWeapon(Weapon weap)
    {
        weapon = weap;
        transform.GetChild(0).GetComponent<Weapon>().holdPosition = weapon.holdPosition;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        //transform.GetChild(0).GetComponent<Animator>().enabled = true;
        transform.GetChild(0).GetComponent<Weapon>().holdPosition = weap.holdPosition;
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        stats.level++;
        stats.maxHitpoints = stats.IncreaseStatByFlatAmount(stats.maxHitpoints, 5f);
        stats.baseDamage = stats.IncreaseStatByFlatAmount(stats.baseDamage, 4f);
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
        return stats.level;
    }

    public void SetLevel(int levelToSet)
    {
        stats.level = levelToSet;
    }

}