using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public fields
    public int hitpoint;//current hitpoints
    public int maxHitpoint;//maximum hitpoints
    public float pushRecoverySpeed = 0.2f;//how long it takes to recover after being knocked back

    //Immunity
    protected float immuneTime = 1.0f;// this is how long you have i-frames
    protected float lastImmune;//tracks when you started immunity

    //Push

protected Vector3 pushDirection; //which direction do you fly

//All fighters can ReceiveDAmage / Die

protected virtual void ReceiveDamage(Damage dmg){

    if(Time.time-lastImmune > immuneTime){//check to see if we are still immune
        lastImmune = Time.time;
        hitpoint -= dmg.damageAmount;
        pushDirection = (transform.position - dmg.origin);// this will make the hit object move AWAY from the dmg.origin(player that hit them.)
    }
}

protected virtual void Death(){

}
}
