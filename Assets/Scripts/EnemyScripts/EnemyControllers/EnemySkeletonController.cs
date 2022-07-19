using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonController : EnemyController
{
    public Vector3 followGoal;
    public float followOffset;
    //public float unstickToWallOnBadFollow;

    //aggressionRange MUST be smaller than follow range(maybe equal, but that is redundant)
    public float aggressionRange;//when the skeleton is within this aggression range it will then begin to hone in more percisely on the player
    protected override void FixedUpdate()
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
     

    }
    protected override void Wander()
    {
        transform.position = Vector2.MoveTowards(currentPosition, wanderGoal, enemy.stats.speed * Time.deltaTime);
        CheckIfWanderComplete(currentPosition, wanderGoal);
        //UnityEngine.Debug.Log("Hit 1");
        if (!chooseNewDirection)
        {
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
    protected override void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, followGoal, enemy.stats.speed * Time.deltaTime);
        CheckIfFollowComplete(currentPosition, followGoal);
        Debug.LogWarning("Hit StartFollow");
        if (!chooseNewDirection && !IsPlayerInRange(aggressionRange))
        {
            StartCoroutine(FollowChooseDirectionWithOffset());//we are already in follow range but are not in aggression range
           
            return;
        }
        else if(!chooseNewDirection && IsPlayerInRange(aggressionRange))// we are in follow range AND in aggression range
        {
            StartCoroutine(FollowChooseDirectionWithOffset());
            
        }
        else if (chooseNewDirection)
        {
            return;
        }

    }
    

    protected virtual IEnumerator FollowChooseDirectionWithOffset()// this loops over all the times within it put together
    {
        chooseNewDirection = true;// we do this so we do not overlap the Choose Direction function with itself
      
        followGoal = new Vector3(Random.Range(GameManager.instance.player.transform.position.x - followOffset, GameManager.instance.player.transform.position.x + followOffset) //x value
                                ,Random.Range(GameManager.instance.player.transform.position.y - followOffset, GameManager.instance.player.transform.position.y + followOffset) //y value
                                , 0);
        yield return new WaitForSeconds(Random.Range(2f, 4f));
    }
    protected virtual IEnumerator FollowChooseDirectionWithoutOffset()
    {
        chooseNewDirection = true;// we do this so we do not overlap the Choose Direction function with itself

        followGoal = new Vector3(GameManager.instance.player.transform.position.x//x value
                                , GameManager.instance.player.transform.position.y//y value
                                , 0);
        yield return new WaitForSeconds(Random.Range(1f, 2f));
    }
    protected virtual void CheckIfFollowComplete(Vector3 currPosition, Vector3 folGoal)
    {
        if (currPosition == folGoal)
        {
            chooseNewDirection = false;
        }
        else
        {
            chooseNewDirection = true;
        }
    }
    protected virtual IEnumerator ChooseDirectionIdle()
    {
        chooseNewDirection = true;// we do this so we do not overlap the Choose Direction function with itself

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
    }
    protected override void OnCollide(Collider2D coll)
    {
        //Debug.Log($"Enemy has collided with {coll.tag}");
        if (coll.tag.Equals("Wall"))
        {
            Debug.Log(" Skeeleton OnCollide Wall true");
            if (state == EnemyState.Idle)
            {
                Debug.LogWarning("Hit1");
                transform.position = Vector2.MoveTowards(currentPosition,
                            new Vector3(Random.Range(transform.position.x - 0.3f, transform.position.x + 0.3f),Random.Range(transform.position.y - 0.3f, transform.position.y + 0.3f) //y value
                               , 0), enemy.stats.speed * Time.deltaTime);

                StartCoroutine(ChooseDirectionIdle());
                Debug.LogWarning("Hit2");
                Idle();
            }
            else if (state == EnemyState.Wander)
            {
                StartCoroutine(ChooseDirection());
            }
            else if (state == EnemyState.Follow)
            {
                StartCoroutine(FollowChooseDirectionWithOffset());
            }

        }
        if (coll.tag.Equals("Player"))
        {
            //Debug.LogWarning($"{this.name} has collided with a {coll.tag}");
            Damage dmg = new Damage()
            {
                damageAmount = enemy.stats.combinedDamage,
                origin = transform.position,
                knockback = knockback
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
