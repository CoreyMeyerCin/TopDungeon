using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Melee,
    Ranged
}
public class EnemyController : MonoBehaviour
{
    GameObject player; // this will point at the player.instance... we should use GameObject for now on instead of public Player player because GameObject has more tools for us to use.
    public EnemyState currState = EnemyState.Idle; //this is the current state that the enemy is in, starts off with Idle until acted upon.
    public EnemyType enemyType; //Melee or ranged for now may add burrow and flying
    public float range;// this will be used to for the enemys sight range
    public float speed =1; // how fast the enemy can move in pixels/ps
    public float attackRange;// this is how far the enemy is able to attack the player, or switch EnemyState to attack
    public float projectileSpeed;// how far the enemy's projectile flies
    private float coolDown;//how often the enemy can use its attack action
    private bool chooseDir = false;// this is how the enemy chooses which way to walk/attack
    private bool dead = false; // checks to see if the enemy is dead
    private bool coolDownAttack = false; // the Time.time check of if enemy can attack again
    private bool notInRoom; //Checks to see if Player is in the same room as the enemy
    private Vector3 randomDir; //sets initial moving direction
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
    private CharacterController controller;
    public float homeStretch;//used for seeing how far we are from home

    private void Awake()
    {
        homePosition = transform.position;
        wanderGoal = homePosition;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//this is why we use GameObject... Using the Tag is strong here
        Debug.Log($"Found Player: {player.name}");
        currentPosition = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = false;
    }


    private void Update()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        //Collision work
        boxCollider.OverlapCollider(filter, hits); //take BoxCollider and look for other collision and put its into the hits[]
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            //Debug.Log(hits[i].name);//this will check all 10 collision slots of our array

            OnCollide(hits[i]);

            //The array is not cleaned up, so we di it ourself
            hits[i] = null;
        }
    }
        private void FixedUpdate()
    {
        UnityEngine.Debug.Log($"Current EnemyState: {currState}");

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
        else if(!IsAwayFromHome(range) && currentPosition == homePosition)
        {
            //UnityEngine.Debug.Log(" Hit C");
            currState = EnemyState.Wander;
            Wander();
        }else if (!IsAwayFromHome(range))
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
    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(currentPosition, player.transform.position) <= range;//this checks to see if the player is within range by taking the players position and our position and comparing them using Vector3.Distance
    }
    private bool IsAwayFromHome(float homeStretch)
    {
        return Vector3.Distance(currentPosition, homePosition) >= homeStretch;
    }

    private IEnumerator ChooseDirection()// this loops over all the times within it put together
    {
        chooseDir = true;// we do this so we do not overlap the Choose Direction function with itself
        //yield return new WaitForSeconds(Random.Range(1f, 4f));// This will make the enemy wait 2-8 seconds before choosign a direction
        wanderGoal = new Vector3(Random.Range(homePosition.x -range, homePosition.x + range), //x value
                                 Random.Range(homePosition.y - range, homePosition.y + range), //y value
                                                0);
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
    private void CheckIfWanderComplete(Vector3 currPosition, Vector3 wanGoal)
    {
        if(currPosition == wanGoal)
        {
            chooseDir = false;
        }
        else
        {
            chooseDir = true;
        }
    }
    void Idle()
    {
        transform.position = Vector2.MoveTowards(currentPosition, homePosition, speed * Time.deltaTime);
        wanderGoal = homePosition;
    }

    void Wander()
    {


        transform.position = Vector2.MoveTowards(currentPosition, wanderGoal, speed * Time.deltaTime);
        CheckIfWanderComplete(currentPosition, wanderGoal);
        //UnityEngine.Debug.Log("Hit 1");
        if (!chooseDir)
        {
            UnityEngine.Debug.Log("Hit 2");
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
  

    void Follow()
    {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);//this is nice 
    }

    void Attack()
    {
        if (!coolDownAttack)
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
    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }


    //public void Death()
    //{
    //we have this in the Enemy.cs
    //}

    private void OnCollide(Collider2D coll)
    {
        UnityEngine.Debug.Log($"Enemy has collided with {coll.tag}");
        if (coll.tag.Equals("Wall"))
        {
            UnityEngine.Debug.Log("OnCollide Wall true");
            StartCoroutine(ChooseDirection());
        }
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