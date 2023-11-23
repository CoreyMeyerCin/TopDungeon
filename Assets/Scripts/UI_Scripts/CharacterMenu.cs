using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, hitpointText, goldText, upgradedCostText, xpText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar; //make the bar work with a transform-scale

    //Character Selection
    public void OnArrowClick(bool right) //refers to the arrows on our menu
    {
        if(right)
        {
            currentCharacterSelection++;
        
            //if we went too far in our array
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
            OnSelectionChanged();
        }
        else
        {
           currentCharacterSelection--;
        
            //if we went too far in our array
            if(currentCharacterSelection <0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }

    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    //public void OnUpgradeClick()
    //{
    //    if(GameManager.instance.TryUpgradeWeapon())
    //    {
    //        UpdateMenu();
    //    }
    //}

    //Update the character Information
    public void UpdateMenu()
    {
        //Weapon
        //weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
        //if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        //{
        //    upgradedCostText.text = "MAX";
        //}
        //else
        //{
        //    upgradedCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        //}
        //upgradedCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //Meta
        levelText.text = Player.instance.GetLevel().ToString();
        hitpointText.text = GameManager.instance.player.stats.hitpoints.ToString();
        goldText.text = Player.instance.gold.ToString();

        //XP Bar
        //int currLevel = ExperienceManager.instance.GetLevel();

        //if(GameManager.instance.GetCurrentLevel() == GameManager.instance.expTable.Count)
        //{
        //    xpText.text = GameManager.instance.experience.ToString() + "Total experience points"; //Display total xp if we are max level
        //    xpBar.localScale = Vector3.one; // this will fill the whole bar
        //}   
        //else
        //{
        //    int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel-1); //the highest we have achieved
        //    int currLevelXp = GameManager.instance.GetXpToLevel(currLevel); // our next level up number

        //    int diff = currLevelXp - prevLevelXp;// how much we need to level up
        //    int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;// how far into this level's xp we are in

        //    float completionRatio = (float)currXpIntoLevel/(float)diff;//Our ratio from current xp to how much is needed
        //    xpBar.localScale = new Vector3(completionRatio,1,1);
        //    xpText.text = currXpIntoLevel.ToString() + "/" + diff; // this is what we want displayed on the XpBar in the Menu
        //}
    }

}
