using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]// lets us look at in the inspector

public class CollectionController : Collidable
{
    GameObject player;
    public string name; //for ui
    public string description;// for ui
    public Sprite itemSprite;
    public float currHitPoint;
    public float maxHitPoint;
    public float moveSpeed;
    public float attackSpeed;
    public float pushRecovery;
    private HealthService healthService;
    public Weapon? weapon;
    public Dagger? projectile;
    public Transform trans;
    public float lifespan;//used for projectile range
    public float critChance; 
    public float critMultiplier;//0.01 = 1% increase. BOTH crit properties are held in the player class
    public float playerDamage;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<SpriteRenderer>().sprite = itemSprite;
        Destroy(GetComponent<BoxCollider2D>());
        gameObject.AddComponent<BoxCollider2D>();
    }

   
    protected override void OnCollide(Collider2D coll)
    {

        if (coll.name.Equals("Player"))
        {

            Player.collectedAmount++;
            UnityEngine.Debug.Log("Collsion here!");
            GameManager.instance.player.CurrentHitPointChange(currHitPoint);//in Player
            GameManager.instance.player.MaxHitPointsChange(maxHitPoint);
            GameManager.instance.player.MoveSpeedChange(moveSpeed);// in Mover
            GameManager.instance.player.AttackSpeedChange(attackSpeed);// in User. Uses multiplication so 0.93 would be a 7% increase
            GameManager.instance.player.PushRecoveryChange(pushRecovery);// in User. Uses multiplication so 0.93 would be a 7% increase

            if (weapon != null)
            {
                GameManager.instance.player.ChangeCurrentWeapon(weapon);
            }
            if (projectile != null)
            {
                GameManager.instance.player.ChangeCurrentProjectile(projectile, weapon);
            }
            GameManager.instance.player.ProjectileLifespanChange(lifespan);
            GameManager.instance.player.CritChanceChange(critChance);
            GameManager.instance.player.CritMultiplierChange(critMultiplier);
            GameManager.instance.player.PlayerDamageChange(playerDamage);
            Destroy(gameObject);
        }
    }
}