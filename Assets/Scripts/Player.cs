using UnityEngine;
using UnityEngine.UI;

public class Player : Fighter
{
    public static Player instance;

    public Vector3 currentPosition;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public Transform firePoint;

    public PlayerAnimationController animationController;
    public PlayerActionController actionController;
    public Weapon weapon;

    public Inventory inventory;

    public HealthService healthService;
    public PlayerSpriteService spriteService;

    public int skinId = 1;
    public double playerDirection;
    public int gold;

    //public Projectile projectilePrefab; //holds the weapons prefab, might be able to do this in a better way to do this in the weapon
    //public Projectile projectilePrefab;
    //public float dashTime = 1f; //how long it takes to complete the full dash
    public Vector3 dashStart, dashEnd;

	public new void Awake()
	{
        animationController = GetComponent<PlayerAnimationController>();
        actionController = GetComponent<PlayerActionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventory = GetComponent<Inventory>();
        base.Awake();
    }

	public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentPosition = transform.position;
        firePoint.position = transform.GetChild(0).GetComponent<Weapon>().holdPosition;
        //playerStats = GetComponent<Stats>();
        PlayerAnimator.SetWeaponAnimationTree();
        //projectilePrefab = GameManager.instance.player.transform.GetChild(0).GetComponent<Projectile>();
    }

    private void OnEnable()
    {
        Actions.OnLevelUp += OnLevelUp;
        Actions.OnWeaponChanged += ChangeCurrentWeapon;
    }

    private void OnDisable()
    {
        Actions.OnLevelUp -= OnLevelUp;
        Actions.OnWeaponChanged -= ChangeCurrentWeapon;
    }

    private void Update()
    {
        GetPlayerDirection();

        //playerAnimator.GetAnimation();
        //firePoint = this.transform;
        //this.GetComponentInChildren<Weapon>().transform.localPosition += new Vector3(0.096f,-0.011f,0);

        //projectilePrefab = GameManager.instance.player.transform.GetChild(0).gameObject.GetComponent<Projectile>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal"); // returns -1,1,or 0 depending if we are using a, d, or no input.
        float y = Input.GetAxisRaw("Vertical"); // same thing with the y axis with w, s, or no input

        //Reset MoveDelta
        //UpdateMotor(new Vector3(x, y, 0));
        currentPosition = transform.position;


    }

    public void ChangeSkinId(int skinChange)
    {
        skinId = skinChange;
    }
    public void SetDashLocationGoal(double playerDir)
    {
        switch (playerDir)
        {
            case 0:
                dashEnd = new Vector3(currentPosition.x + (stats.speed / 5), currentPosition.y, currentPosition.z);
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

    public void ChangeCurrentProjectile(Projectile proj, Weapon weap)
    {
        Debug.Log("wtf happened to my projectiles");
        weapon = weap;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
        transform.GetChild(0).GetComponent<Weapon>().weaponName=weap.weaponName;
        transform.GetChild(0).GetComponent<Weapon>().projectilePrefab = proj;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
        transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
        //transform.GetChild(0).GetComponent<Animator>().enabled = false;
        transform.GetChild(0).GetComponent<Weapon>().holdPosition = weap.holdPosition;
        transform.GetChild(0).GetComponent<Weapon>().animAttackType = weap.animAttackType;
        transform.GetChild(0).GetComponent<Weapon>().animHoldType = weap.animHoldType;
        //transform.GetChild(0).GetComponent<BoxCollider2D>().offset = weap.boxCollider.offset;
        //transform.GetChild(0).GetComponent<BoxCollider2D>().size = weap.boxCollider.size;
        PlayerAnimator.SetWeaponAnimationTree();
    }

    public void ChangeCurrentWeapon(Weapon weap)
    {
        if (weap != null)
		{
            weapon = weap;
            transform.GetChild(0).GetComponent<Weapon>().holdPosition = weapon.holdPosition;
            transform.GetChild(0).GetComponent<Weapon>().weaponName = weap.weaponName;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weap.sprite;
            transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
            transform.GetChild(0).GetComponent<Weapon>().knockBack = weap.knockBack;
            transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
            transform.GetChild(0).GetComponent<Weapon>().baseDamage = weap.baseDamage;
            transform.GetChild(0).GetComponent<Weapon>().weaponType = weap.weaponType;
            //transform.GetChild(0).GetComponent<Animator>().enabled = true;
            transform.GetChild(0).GetComponent<Weapon>().holdPosition = weap.holdPosition;
            transform.GetChild(0).GetComponent<Weapon>().animAttackType = weap.animAttackType;
            transform.GetChild(0).GetComponent<Weapon>().animHoldType = weap.animHoldType;
            //transform.GetChild(0).GetComponent<BoxCollider2D>().offset = weap.boxCollider.offset;
            //transform.GetChild(0).GetComponent<BoxCollider2D>().size = weap.boxCollider.size;
            PlayerAnimator.SetWeaponAnimationTree();

            Debug.Log($"Equipped {weap.weaponName}");
        }
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        stats.level++;
        stats.maxHitpoints = stats.IncreaseByFlatAmount(stats.maxHitpoints, 5f);
        stats.baseDamage = stats.IncreaseByFlatAmount(stats.baseDamage, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Player has collided");
        //if (collision.tag.Equals("Portal"))
        //{
        //    Debug.Log($"{collision}");
        //}
    }

    public double GetPlayerDirection()
    {
        if (Input.anyKey)
        {

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
            {
                playerDirection = 0.5;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            {
                playerDirection = 1.5;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
            {
                playerDirection = 2.5;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            {
                playerDirection = 3.5;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerDirection = 0;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerDirection = 1;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerDirection = 2;
                return playerDirection;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                playerDirection = 3;
                return playerDirection;
            }
        }

        return playerDirection;
    }

    public void Death()
	{
        Debug.Log("Player has died");
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