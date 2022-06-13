using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Collectable
{
    public Sprite[] spriteList;
    public Item[] itemList;
    private int itemIndex;
    private int rollerIntMax;

    public void Awake()
    {
        rollerIntMax = 0;
        foreach(Item item in itemList)
        {
            rollerIntMax++;
        }
        itemIndex=UnityEngine.Random.Range(0, rollerIntMax);

        GetComponent<SpriteRenderer>().sprite = spriteList[itemIndex];

    }

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
        }
    }

}
