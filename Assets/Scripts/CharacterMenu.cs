using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, goldText, upgradedCostText, xpText;

    //Logic Field
    private int currentCharacterSelection =0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;//this will make the bar work with a transform-scale

    //Character Selection
    public void OnArrowClick(bool right){//this refers to the arrows on our menu

        if(right){
            currentCharacterSelection++;
        
            //if we went too far in our array
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count){
            currentCharacterSelection=0;
            }
        OnSelectionChanged();
        }
        else{
           currentCharacterSelection--;
        
            //if we went too far in our array
            if(currentCharacterSelection <0){
            currentCharacterSelection= GameManager.instance.playerSprites.Count-1;
            }
        OnSelectionChanged();
        
        }
    }
    private void OnSelectionChanged(){

        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }
    //Weapon Upgrade
    public void OnUpgradeClick(){
       if(GameManager.instance.TryUpgradeWeapon()){
           UpdateMenu();
       }
    }
    //Update the character Information
    public void UpdateMenu(){
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count){
            upgradedCostText.text = "MAX";
        }
        else{
            upgradedCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        upgradedCostText.text = 
        //Meta
        levelText.text = "NOT IMPLEMENTED";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        goldText.text = GameManager.instance.gold.ToString();

        //XP Bar
        xpText.text = "NOT IMPLEMENTED";
        xpBar.localScale= new Vector3(0.5f,0,0);// this means half way
    }
}
