using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : EnemyController
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
     

    }
    protected override void Wander()
    {
        transform.position = Vector2.MoveTowards(currentPosition, wanderGoal, speed * Time.deltaTime);
        CheckIfWanderComplete(currentPosition, wanderGoal);
        //UnityEngine.Debug.Log("Hit 1");
        if (!chooseDir)
        {
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
    protected override void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, followGoal, speed * Time.deltaTime);
        CheckIfFollowComplete(currentPosition, followGoal);
        Debug.LogWarning("Hit StartFollow");
        if (!chooseDir && !IsPlayerInRange(aggressionRange))
        {
            StartCoroutine(FollowChooseDirectionWithOffset());//we are already in follow range but are not in aggression range
           
            return;
        }
        else if(!chooseDir && IsPlayerInRange(aggressionRange))// we are in follow range AND in aggression range
        {
            StartCoroutine(FollowChooseDirectionWithOffset());
            
        }
        else if (chooseDir)
        {
            return;
        }

    }
    

    protected virtual IEnumerator FollowChooseDirectionWithOffset()// this loops over all the times within it put together
    {
        chooseDir = true;// we do this so we do not overlap the Choose Direction function with itself
      
        followGoal = new Vector3(Random.Range(GameManager.instance.player.transform.position.x - followOffset, GameManager.instance.player.transform.position.x + followOffset) //x value
                                ,Random.Range(GameManager.instance.player.transform.position.y - followOffset, GameManager.instance.player.transform.position.y + followOffset) //y value
                                , 0);
        yield return new WaitForSeconds(Random.Range(2f, 4f));
    }
    protected virtual IEnumerator FollowChooseDirectionWithoutOffset()
    {
        chooseDir = true;// we do this so we do not overlap the Choose Direction function with itself

        followGoal = new Vector3(GameManager.instance.player.transform.position.x//x value
                                , GameManager.instance.player.transform.position.y//y value
                                , 0);
        yield return new WaitForSeconds(Random.Range(1f, 2f));
    }
    protected virtual void CheckIfFollowComplete(Vector3 currPosition, Vector3 folGoal)
    {
        if (currPosition == folGoal)
        {
            chooseDir = false;
        }
        else
        {
            chooseDir = true;
        }
    }
    protected virtual IEnumerator ChooseDirectionIdle()
    {
        chooseDir = true;// we do this so we do not overlap the Choose Direction function with itself

        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
    }
    protected override void OnCollide(Collider2D coll)
    {
        //Debug.Log($"Enemy has collided with {coll.tag}");
        if (coll.tag.Equals("Wall"))
        {
            Debug.Log(" Skeeleton OnCollide Wall true");
            if (currState == EnemyState.Idle)
            {
                Debug.LogWarning("Hit1");
                transform.position = Vector2.MoveTowards(currentPosition,
                            new Vector3(Random.Range(transform.position.x - 0.3f, transform.position.x + 0.3f),Random.Range(transform.position.y - 0.3f, transform.position.y + 0.3f) //y value
                               , 0),speed * Time.deltaTime);

                StartCoroutine(ChooseDirectionIdle());
                Debug.LogWarning("Hit2");
                Idle();
            }
            else if (currState == EnemyState.Wander)
            {
                StartCoroutine(ChooseDirection());
            }
            else if (currState == EnemyState.Follow)
            {
                StartCoroutine(FollowChooseDirectionWithOffset());
            }

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
}
