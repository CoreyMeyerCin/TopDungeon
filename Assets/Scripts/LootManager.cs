using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public GameObject[] dropListCommon;
    public GameObject[] dropListUncommon;
    public GameObject[] dropListRare;
    public GameObject[] dropListUnique;
    public GameObject[] dropListLegendary;


    public int AmountOfGoldDropped(int gold, int enemyLevel)
    {
        int returnGold = ((gold * enemyLevel) + enemyLevel) / (enemyLevel * 2);
        return returnGold;
    }
    public int OnDeathCalculateGoldEarned(int gold, int level)
    {
        var totalGold = gold * level;
        return totalGold;
    }
    public void RollForLootDrop(int level, Vector3 enemyPosition) //all of this really needs to go into an item manager instead + other code that doesn't involve enemy directly
    {
        Item item = new Item();
        item.RollRarity(level);

        Debug.Log($"Dropping {item.rarity} item");
        switch (item.rarity)
        {
            case Item.Rarity.Common:
                {
                    Debug.LogWarning("Common item drop");
                    int itemIndex = Random.Range(0, dropListCommon.Length - 1);
                    Instantiate(dropListCommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Item.Rarity.Uncommon:
                {
                    Debug.LogWarning("Uncommon item drop");
                    int itemIndex = Random.Range(0, dropListUncommon.Length - 1);
                    Instantiate(dropListUncommon[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Item.Rarity.Rare:
                {
                    Debug.LogWarning("Rare item drop");
                    int itemIndex = Random.Range(0, dropListRare.Length - 1);
                    Instantiate(dropListRare[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Item.Rarity.Unique:
                {
                    Debug.LogWarning("Unique item drop");
                    int itemIndex = Random.Range(0, dropListUnique.Length - 1);
                    Instantiate(dropListUnique[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
            case Item.Rarity.Legendary:
                {
                    Debug.LogWarning("Legendary item drop");
                    int itemIndex = Random.Range(0, dropListLegendary.Length - 1);
                    Instantiate(dropListLegendary[itemIndex], enemyPosition, Quaternion.Euler(new Vector3(0, 0, -90)));
                    return;
                }
        }
    }
}
