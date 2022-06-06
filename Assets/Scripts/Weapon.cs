using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint = {1,2,3,4,5,6,7,8,9,10};//amount of damage each weapon with upgrade does
    public float[] pushForce= {2.0f,2.2f,2.4f,2.6f,2.8f,3.0f,3.2f,3.4f,3.6f,3.8f};//how far you push enemy back for each rank

    
    //Upgrade
    public int weaponLevel = 0;//the current level of the weapon, later this will be used to determine what damage point and pushForce equal through logic
    public SpriteRenderer spriteRenderer;//this is to change the look of our weapon when we upgrade

    //Swing
    private Animator anim;//reference to the Animator
    private float cooldown = 0.5f;//how fast can we swing again
    private float lastSwing;//timer on when our last swing was

    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        spriteRenderer=GetComponent<SpriteRenderer>();// this will update our weapon look when we load 'this' in
        anim = GetComponent<Animator>();
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
                //This is a whill make a Damage when we hit a appropriate target
            Damage dmg = new Damage(){
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce=pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage",dmg);// this will send the damage over to the enemy using ReceiveDamage()
        }
        
    }

    private void Swing(){
        anim.SetTrigger("Swing");//this set 'Swing' in our Animator when we call this function, using the SpaceKey(Update() holds the call to this)

    }

    public void UpgradeWeapon(){
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprite[weaponLevel];

        //Change stats
    }

    public void SetWeaponLevel(int level){
        weaponLevel= level;
        this.spriteRenderer.sprite = GameManager.instance.weaponSprite[level];
    }
}
