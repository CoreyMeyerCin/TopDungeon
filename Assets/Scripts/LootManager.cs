using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static readonly int DROP_CHANCE_CEILING = 1001; //several of these fields need to be moved into a Controller class instead
    public static readonly int ITEM_DROP_THRESHOLD = 0; //roll must be between this value and drop_chance_ceiling for an item to drop

    //adjust rarity rates, compared to drop_chance_ceiling
    private static readonly int UNCOMMON_THRESHOLD = 600;
    private static readonly int RARE_THRESHOLD = 800;
    private static readonly int UNIQUE_THRESHOLD = 925;
    private static readonly int LEGENDARY_THRSHOLD = 990;

    public GameObject[] dropListCommon;
    public GameObject[] dropListUncommon;
    public GameObject[] dropListRare;
    public GameObject[] dropListUnique;
    public GameObject[] dropListLegendary;

    public int itemIndex;
    public Sprite[] spriteList;
    public Item[] itemList;
    public LootManager lootManager;
    public Rarity rarity;


    private bool ShouldDropItem()
    {
        return Random.Range(1, LootManager.DROP_CHANCE_CEILING) >= LootManager.ITEM_DROP_THRESHOLD;
    }

    public int UpdateItemIndex()
    {
        int rollerIntMax = 0; //what is this doing?
        foreach (Item item in itemList)
        {
            rollerIntMax++;
        }
        return itemIndex = Random.Range(0, rollerIntMax);
    }

    public void DropLoot(int level, Vector3 enemyPosition, Enemy enemy) //all of this really needs to go into an item manager instead + other code that doesn't involve enemy directly
    {
        RollRarity(level);
        PickLootToDrop(enemyPosition, enemy);
    }
    public void RollRarity(int enemyLevel)
    {
        var dropChanceFloor = Player.instance.stats.dropChanceModifier + (enemyLevel * 2);
        if (dropChanceFloor > DROP_CHANCE_CEILING)
        {
            dropChanceFloor = DROP_CHANCE_CEILING;
        }
        int roll = Random.Range(dropChanceFloor, DROP_CHANCE_CEILING);
        SetRarityFromRoll(roll);
    }
    public void SetRarityFromRoll(int roll)
    {
        if (roll >= LEGENDARY_THRSHOLD)
        {
            rarity = Rarity.Legendary;
        }
        else if (roll >= UNIQUE_THRESHOLD)
        {
            rarity = Rarity.Unique;
        }
        else if (roll >= RARE_THRESHOLD)
        {
            rarity = Rarity.Rare;
        }
        else if (roll >= UNCOMMON_THRESHOLD)
        {
            rarity = Rarity.Uncommon;
        }
        else
        {
            rarity = Rarity.Common;
        }
        Debug.Log($"Rarity Set to:{rarity}");
    }

    
    public void PickLootToDrop(Vector3 enemyPosition, Enemy enemy)
    {
        Player.instance.gold += lootManager.OnDeathCalculateGoldEarned(enemy.stats.goldValue, enemy.stats.level);

        if (!ShouldDropItem())
        {
            Debug.Log("No loot drop this time");
        }
        else
        {
            Debug.Log("Rolling for loot drop");
            Debug.Log($"Current Rarity{rarity}");
        switch (rarity)
        {
            case Rarity.Common:
                {
                    Debug.LogWarning("Common item drop");
                    int itemIndex = Random.Range(0, lootManager.dropListCommon.Length - 1);
                    Instantiate(lootManager.dropListCommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }

            case Rarity.Uncommon:
                {
                    Debug.LogWarning("Uncommon item drop");
                    int itemIndex = Random.Range(0, lootManager.dropListUncommon.Length - 1);
                    Debug.Log($"Item Index:{itemIndex}");
                    Instantiate(lootManager.dropListUncommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Rarity.Rare:
                {
                    Debug.LogWarning("Rare item drop");
                    int itemIndex = Random.Range(0, lootManager.dropListRare.Length - 1);
                    Instantiate(lootManager.dropListRare[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Rarity.Unique:
                {
                    Debug.LogWarning("Unique item drop");
                    int itemIndex = Random.Range(0, lootManager.dropListUnique.Length - 1);
                    Instantiate(lootManager.dropListUnique[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Rarity.Legendary:
                {
                    Debug.LogWarning("Legendary item drop");
                    int itemIndex = Random.Range(0, lootManager.dropListLegendary.Length - 1);
                    Instantiate(lootManager.dropListLegendary[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
        }
        }
    }
    

    public int AmountOfGoldDropped(int gold, int enemyLevel)
    {
        int returnGold = ((gold * enemyLevel) + enemyLevel) / (enemyLevel * 2);
        return returnGold;
    }
    public int OnDeathCalculateGoldEarned(int gold, int level)
    {
        return gold * level;
        
    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Unique,
        Legendary
    }
}
