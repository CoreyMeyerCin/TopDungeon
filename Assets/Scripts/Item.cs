using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Collectable
{
    public static readonly int DROP_CHANCE_CEILING = 1000; //several of these fields need to be moved into a Controller class instead
    public static readonly int ITEM_DROP_THRESHOLD = 900;

    //adjust rarity rates, compared to drop_chance_ceiling
    private static readonly int UNCOMMON_THRESHOLD = 600; 
    private static readonly int RARE_THRESHOLD = 800;
    private static readonly int UNIQUE_THRESHOLD = 925;
    private static readonly int LEGENDARY_THRSHOLD = 990;

    public string name; //for ui
    public string description;// for ui
    public Sprite itemSprite;
    public Sprite[] spriteList;
    public Item[] itemList;
    private int itemIndex;
    private int rollerIntMax;
    public Rarity rarity;


    private void Awake()
    {
        rollerIntMax = 0; //what is this doing?
        foreach(Item item in itemList)
        {
            rollerIntMax++;
        }
        itemIndex = Random.Range(0, rollerIntMax);
    }

	private void Start()
	{
        GetComponent<SpriteRenderer>().sprite = spriteList[itemIndex];
    }

	protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
        }
    }

    public void RollRarity(int enemyLevel)
	{
        var dropChanceFloor = Player.instance.dropChanceModifier + (enemyLevel * 2);
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
            return;
		}

        if (roll >= UNIQUE_THRESHOLD)
		{
            rarity = Rarity.Unique;
            return;
		}

        if (roll >= RARE_THRESHOLD)
		{
            rarity = Rarity.Rare;
            return;
		}

        if (roll >= UNCOMMON_THRESHOLD)
		{
            rarity = Rarity.Uncommon;
            return;
		}

        rarity = Rarity.Common;
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
