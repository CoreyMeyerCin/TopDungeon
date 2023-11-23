using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName ="NewItem", menuName = "Item/Create New Item")]
public class Item : Collectable//ScriptableObject
{
    public string uiName;
    public string uiDescription;
    public Sprite itemSprite;
    //public Sprite[] spriteList;
    //public Item[] itemList;
    private int itemIndex;
    public Rarity rarity;

    public Item(Rarity rollRarity)
    {
        this.rarity = rollRarity;
    }

    private void Awake()
    {
        //int rollerIntMax = 0; //what is this doing?
        //foreach(Item item in itemList)
        //{
        //    rollerIntMax++;
        //}
        //itemIndex = Random.Range(0, rollerIntMax);
    }
	private void Start()
	{
        base.Start();
    }

	protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
        }
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
