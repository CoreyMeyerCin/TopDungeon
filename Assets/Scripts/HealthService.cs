using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthService : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DamageByFlatAmount(int damage)
	{
        if(currentHealth < damage) //prevent hp from going negative
		{
            damage = currentHealth;
		}

        currentHealth -= damage;
        if(currentHealth <= 0)
		{
            //TODO: die
		}
	}

    void DamageByPercentOfMax(int percentDamage)
	{
        var asFlatDamage = maxHealth / percentDamage; //this is only configured for ints atm. Otherwise needs (int)Math.Ceiling(flatDamage) to convert rounded up
        DamageByFlatAmount(asFlatDamage);
	}

    void DamageByPercentOfCurrent(int percentDamage)
	{
        var asFlatDamage = currentHealth / percentDamage;
        if(asFlatDamage < 1)
		{
            asFlatDamage = 1;
		}

        DamageByFlatAmount(asFlatDamage);
    }
    
    void HealByFlatAmount(int healing)
	{
        var healthMissing = maxHealth - currentHealth;

        if(healthMissing == 0) return; //optional minor optimization

        if(healing > healthMissing)
		{
            healing = healthMissing;
		}

        currentHealth += healing;
	}

    void HealByPercentageOfMax(int percentageHealing)
	{
        var asFlatHealing = maxHealth / percentageHealing;
        HealByFlatAmount(asFlatHealing);
	}

    void HealByPercentageOfCurrent(int percentageHealing) //idk if this would ever be useful whatsoever but w/e
	{
        var asFlatHealing = currentHealth / percentageHealing;
        if(asFlatHealing < 1)
		{
            asFlatHealing = 1;
		}

        HealByFlatAmount(asFlatHealing);
	}

}
