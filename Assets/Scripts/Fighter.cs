using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    protected float immunityDuration = 1.0f;
    public bool isImmune;

    protected Vector3 knockbackDirection;

    public Stats stats;

	public void Awake()
	{
        stats = new Stats();
        isImmune = false;
    }

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (!isImmune)
		{
            stats.hitpoints -= dmg.damageAmount;
            knockbackDirection = (transform.position - dmg.origin).normalized * dmg.knockback; //make the hit object move AWAY from the dmg.origin

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);
            StartCoroutine(DoImmunity(immunityDuration));

            if (stats.hitpoints <= 0)
            {
                stats.hitpoints = 0;
                Death();
            }

            StartCoroutine(PushToZero(stats.knockbackRecoverySpeed));
        }

    }

    private IEnumerator PushToZero(float recoverySpeed)
    {
        yield return new WaitForSeconds(recoverySpeed);

        knockbackDirection = (transform.position).normalized * 0;
    }

    IEnumerator DoImmunity(float immunityDuration)
    {
        isImmune = true;
        float timer = 0;
        while (timer < immunityDuration)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        isImmune = false;
    }

    protected virtual void Death()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            gameObject.GetComponent<EnemyController>().Death();
        }

        if (gameObject.CompareTag("Player"))
		{
            gameObject.GetComponent<Player>().Death();
		}
    }

}
