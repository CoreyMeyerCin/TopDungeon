using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Stats :MonoBehaviour
{
    public float AdditionalDamage;
    public float attackSpeed;
    public float baseDamage;
    public float cooldown;
    public float critChance;
    public float critMultiplier;
    public int combinedDamage
	{
        get { return Mathf.RoundToInt(baseDamage + AdditionalDamage); }
    }
    public float dashCooldown;
    public int dropChanceModifier;
    public int goldValue;
    public float hitpoints;
    public float knockback;
    public int level;
    public float lifesteal;
    public float maxHitpoints;
    public float knockbackRecoverySpeed; //how long it takes to recover after being knocked back
    public float speed;
    public int xpValue;

    //USAGE:
    //Player.instance.stats.maxHitpoints = Player.instance.stats.IncreaseStatByFlatAmount(Player.instance.stats.maxHitpoints, 100);
    public int IncreaseStatByFlatAmount(int currentValue, int increaseAmount)
    {
        return currentValue + increaseAmount;
    }
    public float IncreaseStatByFlatAmount(float currentValue, float increaseAmount)
    {
        return currentValue + increaseAmount;
    }

    /// <summary>
    /// Enter the increasePercentage as an integer : 50 -> 50% increase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="currentValue"></param>
    /// <param name="increasePercentage"></param>
    /// <returns></returns>
    public int IncreaseStatByPercentage(int currentValue, int increasePercentage)
    {
        float convertedPercentIncrease = increasePercentage / 100;
        int flattenedIncrease = Mathf.RoundToInt(currentValue * convertedPercentIncrease);
        if (flattenedIncrease < 1)
        {
            flattenedIncrease = 1;
        }

        return IncreaseStatByFlatAmount(currentValue, flattenedIncrease);
    }
    /// <summary>
    /// Enter the increasePercentage as an integer : 50 -> 50% increase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="currentValue"></param>
    /// <param name="increasePercentage"></param>
    /// <returns></returns>
    public float IncreaseStatByPercentage(float currentValue, float increasePercentage)
    {
        float convertedPercentIncrease = increasePercentage / 100;
        float flattenedIncrease = currentValue * convertedPercentIncrease;
        if (flattenedIncrease == 0)
        {
            flattenedIncrease = 0.1f;
        }

        return IncreaseStatByFlatAmount(currentValue, flattenedIncrease);
    }

}
