using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthService : MonoBehaviour
{
    [SerializeField] private static float maxHealth;
    [SerializeField] private static float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DamageByFlatAmount(float damage)
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

    void DamageByPercentOfMax(float percentDamage)
	{
        var asFlatDamage = maxHealth / percentDamage; //this is only configured for ints atm. Otherwise needs (int)Math.Ceiling(flatDamage) to convert rounded up
        DamageByFlatAmount(asFlatDamage);
	}

    void DamageByPercentOfCurrent(float percentDamage)
	{
        var asFlatDamage = currentHealth / percentDamage;
        if(asFlatDamage < 1)
		{
            asFlatDamage = 1;
		}

        DamageByFlatAmount(asFlatDamage);
    }
    
    public void HealByFlatAmount(float healing)
	{
        var healthMissing = maxHealth - currentHealth;

        if(healthMissing == 0) return; //optional minor optimization

        if(healing > healthMissing)
		{
            healing = healthMissing;
		}

        currentHealth += healing;
	}

    void HealByPercentageOfMax(float percentageHealing)
	{
        var asFlatHealing = maxHealth / percentageHealing;
        HealByFlatAmount(asFlatHealing);
	}

    void HealByPercentageOfCurrent(float percentageHealing) //idk if this would ever be useful whatsoever but w/e
	{
        var asFlatHealing = currentHealth / percentageHealing;
        if(asFlatHealing < 1)
		{
            asFlatHealing = 1;
		}

        HealByFlatAmount(asFlatHealing);
	}

}
