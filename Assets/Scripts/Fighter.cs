using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //Public fields
    public float hitpoints; //current hitpoints
    public float maxHitpoints; //maximum hitpoints
    public float pushRecoverySpeed = 0.2f;//how long it takes to recover after being knocked back

    //Immunity
    protected float immuneTime = 1.0f;// this is how long you have i-frames
    protected float lastImmune;//tracks when you started immunity
    //Push

    protected Vector3 pushDirection; //which direction do you fly

    //Attack Stats


    //All fighters can ReceiveDAmage / Die

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime) //check to see if we are still immune
        {
            lastImmune = Time.time;
            hitpoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // this will make the hit object move AWAY from the dmg.origin(player that hit them.)
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
            
            if (hitpoints <= 0)
            {
                hitpoints = 0;
                Death();
            }
        }
        StartCoroutine(PushToZero(pushRecoverySpeed));
    }

    private IEnumerator PushToZero(float recoverySpeed)
    {
        yield return new WaitForSeconds(recoverySpeed);

        pushDirection = (transform.position).normalized * 0;
    }

    protected virtual void Death()
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            //GameManager.instance.experienceManager.OnExperienceChanged((int)gameObject.GetComponent<EnemyController>().exp);
            gameObject.GetComponent<EnemyController>().Death();
            
        }
        Destroy(gameObject);
    }
}
