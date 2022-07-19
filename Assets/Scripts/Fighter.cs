using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    protected float immuneDuration = 1.0f;
    protected float immuneStartedTime;

    protected Vector3 knockbackDirection;

    public Stats stats;

	private void Awake()
	{
        stats = new Stats();
    }

	private void Update()
    {
        if (stats.hitpoints <= 0)
        {
            stats.hitpoints = 0;
            Death();
        }
    }
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - immuneStartedTime > immuneDuration) //check to see if still immune
        {
            immuneStartedTime = Time.time;
            stats.hitpoints -= dmg.damageAmount;
            knockbackDirection = (transform.position - dmg.origin).normalized * dmg.knockback; // this will make the hit object move AWAY from the dmg.origin(player that hit them.)
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
        }
        StartCoroutine(PushToZero(stats.knockbackRecoverySpeed));
    }

    private IEnumerator PushToZero(float recoverySpeed)
    {
        yield return new WaitForSeconds(recoverySpeed);

        knockbackDirection = (transform.position).normalized * 0;
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
