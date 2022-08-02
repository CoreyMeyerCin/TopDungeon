using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static readonly int DROP_CHANCE_CEILING = 1001;
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
    public Rarity rarity;


    public int UpdateItemIndex()
    {
        int rollerIntMax = 0; //what is this doing?
        foreach (Item item in itemList)
        {
            rollerIntMax++;
        }
        return itemIndex = Random.Range(0, rollerIntMax);
    }

    public void DropLoot(int level, Vector3 enemyPosition, Enemy enemy)
    {
        RollRarity(level);
        PickLootToDrop(enemyPosition, enemy);
    }

    public void PickLootToDrop(Vector3 enemyPosition, Enemy enemy)
    {
        Player.instance.gold += OnDeathCalculateGoldEarned(enemy.stats.goldValue, enemy.stats.level);

        if (ShouldDropItem())
        {
            Debug.Log($"Rolling for {rarity} loot drop");
            switch (rarity)
            {
                case Rarity.Common:
                    {
                        Debug.LogWarning("Common item drop");
                        int itemIndex = Random.Range(0, dropListCommon.Length - 1);
                        Instantiate(dropListCommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }

                case Rarity.Uncommon:
                    {
                        Debug.LogWarning("Uncommon item drop");
                        int itemIndex = Random.Range(0, dropListUncommon.Length - 1);
                        Debug.Log($"Item Index:{itemIndex}");
                        Instantiate(dropListUncommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Rarity.Rare:
                    {
                        Debug.LogWarning("Rare item drop");
                        int itemIndex = Random.Range(0, dropListRare.Length - 1);
                        Instantiate(dropListRare[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Rarity.Unique:
                    {
                        Debug.LogWarning("Unique item drop");
                        int itemIndex = Random.Range(0, dropListUnique.Length - 1);
                        Instantiate(dropListUnique[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
                case Rarity.Legendary:
                    {
                        Debug.LogWarning("Legendary item drop");
                        int itemIndex = Random.Range(0, dropListLegendary.Length - 1);
                        Instantiate(dropListLegendary[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                        return;
                    }
            }
        }

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
        Debug.Log($"Drop rarity: rolled {roll} - {rarity}");
    }

    private bool ShouldDropItem()
    {
        var itemRoll = Random.Range(1, DROP_CHANCE_CEILING); //TODO: '1' here will be replaced with base drop chance + players drop chance
        Debug.Log($"Rolled {itemRoll} - drop threshold is between {ITEM_DROP_THRESHOLD} and {DROP_CHANCE_CEILING}");
        return itemRoll >= ITEM_DROP_THRESHOLD;
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
