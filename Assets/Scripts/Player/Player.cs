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

	public new void Awake()
	{
        animationController = GetComponent<PlayerAnimationController>();
        actionController = GetComponent<PlayerActionController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    }
    public void ChangeSkinId(int skinChange)
    {
        skinId = skinChange;
    }

    public void ChangeCurrentProjectile(Projectile proj, Weapon weap)
    {
        transform.GetChild(0).GetComponent<Weapon>().projectilePrefab = proj;
        ChangeCurrentWeapon(weap);
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
            //Debug.Log($"Equipped {weap.weaponName}");
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