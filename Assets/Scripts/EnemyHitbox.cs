using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage =1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player"){

            //Create a new damage object, before sending it to the player
                //This is a whill make a Damage when we hit a appropriate target
            Damage dmg = new Damage(){
                damageAmount = damage,
                origin = transform.position,
                pushForce=pushForce
            };

            coll.SendMessage("ReceiveDamage",dmg);// this will send the damage over to the enemy using ReceiveDamage()
        }
    }
}
