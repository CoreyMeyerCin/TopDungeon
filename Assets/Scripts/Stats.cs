using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float additionalDamage { get; set; } = 0;
    public float attackSpeed { get; set; } = 1f;
    public float baseDamage { get; set; } = 1;
    public float cooldown { get; set; } = 1f;
    public float critChance { get; set; } = 5f;
    public float critMultiplier { get; set; } = 1.05f;
    public float combinedDamage
	{
        get { return baseDamage + additionalDamage; }
    }
    public float dashTime { get; set; } = 1f;
    public int dropChanceModifier { get; set; } = 0;
    public int goldValue { get; set; } = 10;
    public float hitpoints { get; set; } = 1f;
    public int level { get; set; } = 1;
    public float lifesteal { get; set; } = 0;
    public float maxHitpoints { get; set; } = 10f;
    public float pushRecoverySpeed { get; set; }  = 0.2f; //how long it takes to recover after being knocked back
    public float speed { get; set; } = 1f;
    public int xpValue { get; set; } = 1;


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
