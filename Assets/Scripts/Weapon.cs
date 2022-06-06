using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int damagePoint = 1;//amount of damage that this weapon does
    public float pushForce= 2.0f;//how far you push enemy back
    
    //Upgrade
    public int weaponLevel = 0;//the current level of the weapon, later this will be used to determine what damage point and pushForce equal through logic
    private SpriteRenderer spriteRenderer;//this is to change the look of our weapon when we upgrade

    //Swing
    private float cooldown = 0.5f;//how fast can we swing again
    private float lastSwing;//timer on when our last swing was


    protected override void Start()
    {
        base.Start();
        spriteRenderer=GetComponent<SpriteRenderer>();// this will update our weapon look when we load 'this' in
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastSwing > cooldown){// when we press the space button we will set lastSwing to the current time(Time.time)
                                                        //this only happens if the last time we used Swing is greater than our cooldown.
                                                            // that is how the cooldown actually exists.
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag=="Fighter"){

            if(coll.name=="Player"){
                return;//if we collide with Player we do nothing(because that is silly)
            }

            //Create a new damage object, then we'll send it to the fighter we've hit
                //This is a whill make a Damage when we hit a appropriate targer
            Damage dmg = new Damage(){
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce=pushForce
            };

            coll.SendMessage("ReceiveDamage",dmg);// this will send the damage over to the enemy using ReceiveDamage()
        }
        
    }

    private void Swing(){
        UnityEngine.Debug.Log("Swing");
    }


}
