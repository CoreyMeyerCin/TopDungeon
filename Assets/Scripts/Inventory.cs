using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int gold;
    //public int wood;
    //public int stone;
    //public int ironOre;
    //public int ironBar;

    public Text goldDisplay;
    //public Text woodDisplay;
    //public Text stoneDisplay;
    //public Text ironOreDisplay;
    //public Text ironBarDisplay;

    public void Update()
    {
        goldDisplay.text = gold.ToString();
        //woodDisplay.text = wood.ToString();
        //stoneDisplay.text = stone.ToString();
        //ironOreDisplay.text = ironOre.ToString();
        //ironBarDisplay.text = ironBar.ToString();
    }
}
