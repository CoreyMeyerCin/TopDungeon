using System.Collections;
using System.Linq;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    GameObject player; // this will point at the player.instance... we should use GameObject for now on instead of public Player player because GameObject has more tools for us to use.
    public EnemyState state = EnemyState.Idle;
    public EnemyType type;//Melee or ranged for now may add burrow and flying

    //many of these fields need to be migrated to Enemy.cs
    public int knockback;
    public float sightRange;
    public float attackRange;
    public float projectileRange;
    protected float attackCooldown;
    protected Vector3 initialMoveDirection;
    protected bool chooseNewDirection = false;
    protected bool isDead = false; //TODO: is this necessary?
    protected bool attackOnCooldown = false;
    protected bool playerNotInRoom;
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
    public float distanceFromHome;
    public bool collidingWithPlayer;
    protected CharacterController controller;
    public Fighter fighter; //TODO: refactor this so it just directly accesses the Enemy.cs sibling script on the GameObject this script is on
    public LootManager lootManager;

    //TODO: move into item controller and loot into proper drop-tables
 

    protected virtual void Awake()
    {
        homePosition = transform.position;
        wanderGoal = homePosition;
    }

    protected virtual void Start()
    {
        lootManager = GameObject.FindObjectOfType<LootManager>();
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
        if (IsPlayerInRange(sightRange))
        {
            //UnityEngine.Debug.Log(" Hit A");
            state = EnemyState.Follow;
            Follow();
        }
        else if (IsAwayFromHome(sightRange))
        {
            //UnityEngine.Debug.Log(" Hit B");
            state = EnemyState.Idle;
            Idle();
        }
        else if (!IsAwayFromHome(sightRange) && currentPosition == homePosition)
        {
            //UnityEngine.Debug.Log(" Hit C");
            state = EnemyState.Wander;
            Wander();
        }
        else if (!IsAwayFromHome(sightRange))
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
        chooseNewDirection = true;// we do this so we do not overlap the Choose Direction function with itself
        //yield return new WaitForSeconds(Random.Range(1f, 4f));// This will make the enemy wait 2-8 seconds before choosign a direction
        wanderGoal = new Vector3(Random.Range(homePosition.x - sightRange, homePosition.x + sightRange) //x value
                                , Random.Range(homePosition.y - sightRange, homePosition.y + sightRange) //y value
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
    protected virtual void CheckIfWanderComplete(Vector3 currPosition, Vector3 wanderGoal)
    {
        if (currPosition == wanderGoal)
        {
            chooseNewDirection = false;
        }
        else
        {
            chooseNewDirection = true;
        }
    }

    protected virtual void Idle()
    {
        
        transform.position = Vector2.MoveTowards(currentPosition, homePosition, fighter.stats.speed * Time.deltaTime);
        wanderGoal = homePosition;
    }

    protected virtual void Wander()
    {
        transform.position = Vector2.MoveTowards(currentPosition, wanderGoal, fighter.stats.speed * Time.deltaTime);
        CheckIfWanderComplete(currentPosition, wanderGoal);
        //UnityEngine.Debug.Log("Hit 1");
        if (!chooseNewDirection)
        {
            Debug.Log("Hit 2");
            StartCoroutine(ChooseDirection());
            return;
        }
        else if (chooseNewDirection)
        {
            //UnityEngine.Debug.Log("Hit 3");
            //transform.position += -transform.right * speed * Time.deltaTime;
            return;
        }
    }


    protected virtual void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, fighter.stats.speed * Time.deltaTime);
    }

    protected virtual void Attack()
    {
        if (!attackOnCooldown)
        {
            switch (type)
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
        yield return new WaitForSeconds(attackCooldown);
        attackOnCooldown = false;
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        //Debug.Log($"Enemy has collided with {coll.tag}");
        if (coll.tag.Equals("Wall"))
        {
            //Debug.Log(" Skeeleton OnCollide Wall true");
            if(state == EnemyState.Wander)
            {
                StartCoroutine(ChooseDirection());
            }
           
        }
        if (coll.tag.Equals("Player"))
        {
            //Debug.LogWarning($"{this.name} has collided with a {coll.tag}");
            Damage dmg = new Damage()
            {
                damageAmount = fighter.stats.combinedDamage,
                origin = transform.position,
                pushForce = knockback
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    public void Death() //TODO: refactor this so it only triggers an OnEnemyKill event and the logic is handled through those events
    {
        Debug.LogWarning($"Death Happened for {this.gameObject}");
        GameManager.instance.experienceManager.OnExperienceChanged(OnDeathCalculateExperienceEarned());
        Player.instance.gold += lootManager.OnDeathCalculateGoldEarned(fighter.stats.goldValue, fighter.stats.level);
        if (ShouldDropItem())
        {
            Debug.LogWarning("Going to roll for loot drop");
            lootManager.RollForLootDrop(fighter.stats.level, this.currentPosition);
        }
        Debug.LogWarning("Destroying myself");
        Destroy(this.gameObject);
    }

    private bool ShouldDropItem()
    {
        //Debug.LogWarning($"Rolling for item drop chance...");
        return Random.Range(1, Item.DROP_CHANCE_CEILING) >= Item.ITEM_DROP_THRESHOLD;
    }
    public int OnDeathCalculateExperienceEarned()
    {
        var totalExperience = Mathf.RoundToInt((fighter.stats.xpValue * fighter.stats.level) / (1 + fighter.stats.level));
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