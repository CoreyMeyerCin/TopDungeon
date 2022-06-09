using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
   //Experience
   public int xpValue = 1;

   //Logic for moving
   public float triggerLength = 1.0f;//how close until the enemy gets triggered to start chasing
   public float chaseLength = 5;//how far the enemy will chase you from their resting position
   private bool chasing;
   public bool collidingWithPlayer;
   private Transform playerTransform;//the players current position
   private Vector3 startingPosition;//their resting position

   //Hitbox
   public ContactFilter2D filter;
   private BoxCollider2D hitbox;
   private Collider2D[] hits = new Collider2D[10];// we can only inherit from 1 thing at a time so we will make a collider starting here

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();// this will go to the Enemy's first child(GetChild(0)) and then gets its BoxCollider2D

    }

    protected void FixedUpdate()
    {
        //Is the player in range for chasing?
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {

            if(Vector3.Distance(playerTransform.position, startingPosition)< triggerLength)
            {
                chasing = true;
            }

            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                
            }
            else
            {
                UpdateMotor(startingPosition-transform.position); //go back to where we were
            }

        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing=false;//this works for anytime the player is out of range, it will make our chasing go to false again
        }
        //Check for overlaps
        collidingWithPlayer =false;// sets this to false at the start
        //Collision work
        boxCollider.OverlapCollider(filter, hits);//this takes the BoxCollider and looks for other collision and put its into the hits[]
        for (int i = 0; i < hits.Length; i++)
        {

            if (hits[i] == null)
            {
                continue;
            }

            //Debug.Log(hits[i].name);//this will check all 10 collision slots of our array

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //The array is not cleaned up, so we di it ourself
            hits[i] = null;
            
        }

        UpdateMotor(Vector3.zero);
    }
    protected override void Death()
    {
        Destroy(gameObject);

        GameManager.instance.GrantXp(xpValue);// where xp is granted
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
