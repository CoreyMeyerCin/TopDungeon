using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Mover
{
	public float aggroZoneSize = 1.0f;
    public float aggroChaseDistance = 5;
    public bool collidingWithPlayer;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10]; // TODO: refactor this onto Fighter so all enemies + players inherit

    protected override void Start()
    {
        base.Start();
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>(); // this will go to the Enemy's first child(GetChild(0)) and then gets its BoxCollider2D

    }

    protected void FixedUpdate()
    {
        ////Is the player in range for chasing?
        //if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        //{

        //    if(Vector3.Distance(playerTransform.position, startingPosition)< triggerLength)
        //    {
        //        chasing = true;
        //    }

        //    if(chasing)
        //    {
        //        if(!collidingWithPlayer)
        //        {
        //            UpdateMotor((playerTransform.position - transform.position).normalized);
        //        }
                
        //    }
        //    else
        //    {
        //        UpdateMotor(startingPosition-transform.position); //go back to where we were
        //    }

        //}
        //else
        //{
        //    UpdateMotor(startingPosition - transform.position);
        //    chasing=false;//this works for anytime the player is out of range, it will make our chasing go to false again
        //}

        boxCollider.OverlapCollider(filter, hits);
        //if (hits.Any(x => x != null && x.CompareTag("Player"))) //return to this later, not working atm, other bugs in the way
        //{
        //    collidingWithPlayer = true;
        //}
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            //Debug.Log(hits[i].name);//this will check all 10 collision slots of our array

            if (hits[i].CompareTag("Player"))
			{
                collidingWithPlayer = true;
            }

            //The array is not cleaned up, so we di it ourself
            hits[i] = null;

        }

        //UpdateMotor(Vector3.zero);
    }

}
