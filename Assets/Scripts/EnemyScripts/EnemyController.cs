using System.Collections;
using System.Linq;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    GameObject player; // this will point at the player.instance... we should use GameObject for now on instead of public Player player because GameObject has more tools for us to use.
    public EnemyState currState = EnemyState.Idle; //this is the current state that the enemy is in, starts off with Idle until acted upon.
    public EnemyType enemyType; //Melee or ranged for now may add burrow and flying

    //many of these fields need to be migrated to Enemy.cs
    public int damage;
    public int knockback;
    public float range; // used for the enemys sight range
    public float speed = 1; // how fast the enemy can move in pixels/ps
    public float attackRange;// how far the enemy is able to attack the player, or switch EnemyState to attack
    public float projectileSpeed; // how far the enemy's projectile flies
    protected float coolDown; // how often the enemy can use its attack action
    protected bool chooseDir = false; // this is how the enemy chooses which way to walk/attack
    protected bool dead = false; // checks to see if the enemy is dead
    protected bool attackOnCooldown = false; // the Time.time check of if enemy can attack again
    protected bool notInRoom; // Checks to see if Player is in the same room as the enemy
    protected Vector3 randomDir; // sets initial moving direction
    public GameObject bulletPrefab; // put the instance of the bullet here, this allows us to use magic and projectiles the same way. We just have to build the prefabs to do what we want.
    //Damage dmg;
    public BoxCollider2D boxCollider;
    public Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;
    public float wanderCooldown = 1f;
    public float wanderStart;
    public float wanderRange;
    public Vector3 wanderGoal;
    public Vector3 wanderOldGoal;
    public Vector3 currentPosition;
    public Vector3 homePosition;
    protected CharacterController controller;
    public float homeStretch;//used for seeing how far we are from home
    public bool collidingWithPlayer;
    public int xpValue;
    public int level;
    public int goldValue;
    public Fighter fighter;

    //TODO: move into item controller and look into proper drop-tables
    public GameObject[] dropListCommon;
    public GameObject[] dropListUncommon;
    public GameObject[] dropListRare;
    public GameObject[] dropListUnique;
    public GameObject[] dropListLegendary;

    protected virtual void Awake()
    {
        homePosition = transform.position;
        wanderGoal = homePosition;
    }

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //this is why we use GameObject... Using the Tag is strong here
        Debug.Log($"Found Player: {player.name}");
        currentPosition = transform.position;
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = false;
    }


    protected virtual void Update()
    {
        boxCollider.OverlapCollider(filter, hits); //take BoxCollider and look for other collision and put its into the hits[]

        if (hits.Where(x => x != null).Any())
        {
            var validHits = hits.Where(x => x != null).ToList(); //ToList is what actually triggers the work of creating the list, so we want to do it here instead of on the null check with .Any()
            System.Array.Clear(hits, 0, hits.Length);
            foreach (var hit in validHits)
            {
                OnCollide(hit);
            }
        }

    }

    protected virtual void FixedUpdate()
    {
        //Debug.Log($"Current EnemyState: {currState}");

        currentPosition = transform.position;
        if (IsPlayerInRange(range))
        {
            //UnityEngine.Debug.Log(" Hit A");
            currState = EnemyState.Follow;
            Follow();
        }
        else if (IsAwayFromHome(range))
        {
            //UnityEngine.Debug.Log(" Hit B");
            currState = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(range) && currentPosition == homePosition)
        {
            //UnityEngine.Debug.Log(" Hit C");
            currState = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(range))
        {
            //UnityEngine.Debug.Log(" Hit D");
            Wander();
        }
        //switch(currState)
        //{
        //    case (EnemyState.Idle):
        //           // UnityEngine.Debug.Log("Idle");
        //        Idle();   // Right now enemies just kind of aimlessly wander. If we want them to stand still we can make this
        //        break;

        //    case (EnemyState.Wander):
        //            //UnityEngine.Debug.Log("Wander");
        //            Wander();
        //        break;

        //    case (EnemyState.Follow):
        //            //UnityEngine.Debug.Log("Follow");
        //            Follow();
        //        break;

        //    //case (EnemyState.Die)://we already have this in the Enemy.cs but might want to transfer it to here
        //    //    break;
        //    case (EnemyState.Attack)://currently this doesnt do anything, once we have ranged enemies it will though
        //            //UnityEngine.Debug.Log("Attack");
        //            Attack();
        //        break;
        //}
    }

    protected virtual bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(currentPosition, player.transform.position) <= range; //check if player is within range by taking the players position and our position and comparing them using Vector3.Distance
    }

    protected virtual bool IsAwayFromHome(float homeStretch)
    {
        return Vector3.Distance(currentPosition, homePosition) >= homeStretch;
    }

    protected virtual IEnumerator ChooseDirection()// this loops over all the times within it put together
    {
        chooseDir = true;// we do this so we do not overlap the Choose Direction function with itself
        //yield return new WaitForSeconds(Random.Range(1f, 4f));// This will make the enemy wait 2-8 seconds before choosign a direction
        wanderGoal = new Vector3(Random.Range(homePosition.x - range, homePosition.x + range) //x value
                                , Random.Range(homePosition.y - range, homePosition.y + range) //y value
                                , 0);
        //wanderGoal = Random.insideUnitCircle * range;
        //UnityEngine.Debug.Log($"Current Position: {currentPosition}\nwanderGoal:{wanderGoal}");
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        //Vector3 smoothLookAt = Vector3.Slerp(wanderOldGoal, wanderGoal, speed *Time.deltaTime);
        //smoothLookAt.y = wanderGoal.y;

        //wanderGoal = Random.insideUnitCircle * wanderRange;
        //wanderOldGoal = currentPosition;
        //currentPosition = new Vector3(wanderGoal.x, currentPosition.y, wanderGoal.y);
        //transform.LookAt(smoothLookAt);
        //controller.SimpleMove(transform.forward * speed);

        //randomDir = new Vector3(0,0,Random.Range(0,360));// this will set their walking direction to a random direction
        //Quaternion nextRotation = Quaternion.Euler(randomDir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));// This will set the location direction of the next motion == somewhere that is not the direction they are headed between 0.5 and 2.5 seconds


    }
    protected virtual void CheckIfWanderComplete(Vector3 currPosition, Vector3 wanGoal)
    {
        if (currPosition == wanGoal)
        {
            chooseDir = false;
        }
        else
        {
            chooseDir = true;
        }
    }

    protected virtual void Idle()
    {
        transform.position = Vector2.MoveTowards(currentPosition, homePosition, speed * Time.deltaTime);
        wanderGoal = homePosition;
    }

    protected virtual void Wander()
    {
        transform.position = Vector2.MoveTowards(currentPosition, wanderGoal, speed * Time.deltaTime);
        CheckIfWanderComplete(currentPosition, wanderGoal);
        //UnityEngine.Debug.Log("Hit 1");
        if (!chooseDir)
        {
            Debug.Log("Hit 2");
            StartCoroutine(ChooseDirection());
            return;
        }
        else if (chooseDir)
        {
            //UnityEngine.Debug.Log("Hit 3");
            //transform.position += -transform.right * speed * Time.deltaTime;
            return;
        }
    }


    protected virtual void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    protected virtual void Attack()
    {
        if (!attackOnCooldown)
        {
            switch (enemyType)
            {
                case (EnemyType.Melee):
                    Follow();
                    StartCoroutine(CoolDown());
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    break;
            }
        }
    }

    protected virtual IEnumerator CoolDown()
    {
        attackOnCooldown = true;
        yield return new WaitForSeconds(coolDown);
        attackOnCooldown = false;
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        //Debug.Log($"Enemy has collided with {coll.tag}");
        if (coll.tag.Equals("Wall"))
        {
            //Debug.Log("OnCollide Wall true");
            StartCoroutine(ChooseDirection());
        }
        if (coll.tag.Equals("Player"))
        {
            //Debug.LogWarning($"{this.name} has collided with a {coll.tag}");
            Damage dmg = new Damage()
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = knockback
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void Death()
    {
        Debug.LogWarning("Death Happened");
        GameManager.instance.experienceManager.OnExperienceChanged(xpValue);
        GameManager.instance.experienceManager.OnExperienceChanged(OnDeathCalculateExperienceEarned());
        Player.instance.gold += OnDeathCalculateGoldEarned();
        RollForLootDrop();
        Destroy(this.gameObject);
    }

    protected virtual void RollForLootDrop() //all of this really needs to go into an item manager instead + other code that doesn't involve enemy directly
    {
        if (ShouldDropItem())
        {
            Item item = new Item();
            item.RollRarity(level);

            switch (item.rarity)
            {
                case Item.Rarity.Common:
                    {
                        Debug.LogWarning("Common item drop");
                        int itemIndex = Random.Range(0, dropListCommon.Length - 1);
                        Instantiate(dropListCommon[itemIndex], this.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Item.Rarity.Uncommon:
                    {
                        Debug.LogWarning("Uncommon item drop");
                        int itemIndex = Random.Range(0, dropListUncommon.Length - 1);
                        Instantiate(dropListUncommon[itemIndex], this.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Item.Rarity.Rare:
                    {
                        Debug.LogWarning("Rare item drop");
                        int itemIndex = Random.Range(0, dropListRare.Length - 1);
                        Instantiate(dropListRare[itemIndex], this.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Item.Rarity.Unique:
                    {
                        Debug.LogWarning("Unique item drop");
                        int itemIndex = Random.Range(0, dropListUnique.Length - 1);
                        Instantiate(dropListUnique[itemIndex], this.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Item.Rarity.Legendary:
                    {
                        Debug.LogWarning("Legendary item drop");
                        int itemIndex = Random.Range(0, dropListLegendary.Length - 1);
                        Instantiate(dropListLegendary[itemIndex], this.transform.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
            }
        }

    }


    private bool ShouldDropItem()
    {
        Debug.LogWarning($"Rolling for item drop chance...");
        return Random.Range(1, Item.DROP_CHANCE_CEILING) >= Item.ITEM_DROP_THRESHOLD;
    }

    public int OnDeathCalculateGoldEarned()
    {
        var totalGold = goldValue * level;
        return totalGold;
    }

    public int OnDeathCalculateExperienceEarned()
    {
        var totalExperience = Mathf.RoundToInt((xpValue * level) / (1 + level));
        return totalExperience;
    }

    public enum EnemyState //these should probably be inside of Enemy
    {
        Idle,
        Wander,
        Follow,
        Die,
        Attack
    }

    public enum EnemyType
    {
        Melee,
        Ranged,
        Caster
    }
}



///////////////////////////////////////////////////////////
///////////////////////ARCHIVED METHODS////////////////////
//private void ReturnHome()
//{
//    transform.position = Vector3.MoveTowards(transform.position, homePosition, speed * Time.deltaTime);
//}
//private void SetWander()
//{
//    wanderGoal = Vector3.MoveTowards(currentPosition, new Vector3(homePosition.x += Random.Range(-1, 1), homePosition.y += Random.Range(-1,1), 0), speed*Time.deltaTime);
//}