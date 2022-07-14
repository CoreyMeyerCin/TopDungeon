using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    protected float immuneTime = 1.0f; // i-frame duration
    protected float lastImmune; //tracks when immunity started

    protected Vector3 pushDirection; //which direction do you fly

    public Stats stats;

	private void Awake()
	{
        stats = new Stats();
    }

	private void Update()
    {
        if (stats.hitpoints <= 0)
        {
            Debug.LogWarning("Enemy Hitpoints below 0");
            stats.hitpoints = 0;
            Death();
        }
    }
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime) //check to see if still immune
        {
            lastImmune = Time.time;
            stats.hitpoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce; // this will make the hit object move AWAY from the dmg.origin(player that hit them.)
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
            
           
        }
        StartCoroutine(PushToZero(stats.pushRecoverySpeed));
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
    }
}
