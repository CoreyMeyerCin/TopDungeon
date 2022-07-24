using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Stats :MonoBehaviour
{
    //TODO: consider having base stats and bonus stats for all values. Levels scale base values
    //then calculated fields that combine base and bonus to get the effective stat value. Probably only necessary to add as they become useful.
    public float baseDamage;
    public float additionalDamage;
    public int effectiveDamage
	{
        get { return Mathf.RoundToInt(baseDamage + additionalDamage); }
    }
    public float attackSpeed;
    public float cooldown;
    public float critChance;
    public float critMultiplier;
    public float dashTimeLength;
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
    public int IncreaseByFlatAmount(int currentValue, int increaseAmount)
    {
        return currentValue + increaseAmount;
    }
    public float IncreaseByFlatAmount(float currentValue, float increaseAmount)
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
    public int IncreaseByPercentageOfCurrent(int currentValue, int increasePercentage)
    {
        float convertedPercentIncrease = increasePercentage / 100;
        int flattenedIncrease = Mathf.RoundToInt(currentValue * convertedPercentIncrease);
        if (flattenedIncrease < 1)
        {
            flattenedIncrease = 1;
        }

        return IncreaseByFlatAmount(currentValue, flattenedIncrease);
    }
    /// <summary>
    /// Enter the increasePercentage as an integer : 50 -> 50% increase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="currentValue"></param>
    /// <param name="increasePercentage"></param>
    /// <returns></returns>
    public float IncreaseByPercentageOfCurrent(float currentValue, float increasePercentage)
    {
        float convertedPercentIncrease = increasePercentage / 100;
        float flattenedIncrease = currentValue * convertedPercentIncrease;
        if (flattenedIncrease == 0)
        {
            flattenedIncrease = 1f;
        }

        return IncreaseByFlatAmount(currentValue, flattenedIncrease);
    }

    /// <summary>
    /// Enter the increasePercentage as an integer : 50 -> 50% increase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseValue"></param>
    /// <param name="increasePercentage"></param>
    /// <returns></returns>
    public int IncreaByPercentageOfBase(int baseValue, int increasePercentage)
	{
        float convertedPercentIncrease = increasePercentage / 100;
        int flattenedIncrease = Mathf.RoundToInt(baseValue * convertedPercentIncrease);
        if (flattenedIncrease < 1)
        {
            flattenedIncrease = 1;
        }

        return IncreaseByFlatAmount(baseValue, flattenedIncrease);
    }
    /// <summary>
    /// Enter the increasePercentage as an integer : 50 -> 50% increase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="baseValue"></param>
    /// <param name="increasePercentage"></param>
    /// <returns></returns>
    public float IncreaseByPercentageOfBase(float baseValue, float increasePercentage)
    {
        float convertedPercentIncrease = increasePercentage / 100;
        float flattenedIncrease = baseValue * convertedPercentIncrease;
        if (flattenedIncrease == 0)
        {
            flattenedIncrease = 1f;
        }

        return IncreaseByFlatAmount(baseValue, flattenedIncrease);
    }
}
