using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int goldAmount = 10;  

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Player.instance.gold += goldAmount;
                                                                                                     //      Vector3.up*50 says I want to go up and move 50px/second
            GameManager.instance.ShowText("+ " + goldAmount + "gold!", 25, Color.yellow, transform.position, Vector3.up*25f, 3.0f); //new Color(rgb) for custom colors
        }
    }
    
}
