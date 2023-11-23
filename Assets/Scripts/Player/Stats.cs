using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Stats
{
    //TODO: consider having base stats and bonus stats for all values. Levels scale base values
    //then calculated fields that combine base and bonus to get the effective stat value. Probably only necessary to add as they become useful.
    public float baseDamage = 5;
    public float additionalDamage = 0;
    public int effectiveDamage
	{
        get { return Mathf.RoundToInt(baseDamage + additionalDamage); }
    }
    public float attackSpeed = 1;
    public float cooldown = 1;
    public float critChance = 1;
    public float critMultiplier = 2;
    public float dashTimeLength = 0.2f;
    public float dashCooldown = 1;

    public int dropChanceModifier = 0;
    public int goldValue = 10;
    public float hitpoints = 10;
    public float knockback = 1;
    public int level = 1;
    public float lifesteal = 0;
    public float maxHitpoints = 10;
    public float knockbackRecoverySpeed = 1; //how long it takes to recover after being knocked back
    public float speed = 1;
    public int xpValue = 3;

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
