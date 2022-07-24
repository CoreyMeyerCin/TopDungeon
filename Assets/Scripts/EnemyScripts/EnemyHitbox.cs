using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            var enemy = coll.GetComponent<Enemy>();
            Damage dmg = new Damage()
            {
                damageAmount = enemy.stats.effectiveDamage,
                origin = transform.position,
                knockback = enemy.stats.knockback
            };

            coll.SendMessage("ReceiveDamage", dmg); // this will send the damage over to the enemy using ReceiveDamage()
        }
    }
}
