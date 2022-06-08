using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;//by making this static EVERY other class in our code may call GameManager without GetComponent<GameManager>(); use GameManager.instance
    private void Awake(){
        this.InstantiateController();


    }
    private void InstantiateController()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(this.player.gameObject);
            DontDestroyOnLoad(this.floatingTextManager);
        }
        else if(instance != null){
            Debug.Log("Destroying extra GameManager");
            Destroy(this.gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
        }

        //instance = this;
        //DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += LoadState;
    }
    

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    //Logic
    public int gold;
    public int experience;


//Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

        floatingTextManager.Show(msg,fontSize,color,position,motion,duration);//we do this like this because we dont want to have a reference to FloatingTextManager everywhere
                                                                                //Since GameManager is static everywhere else can call FloatingTextManager now
    }


    //Upgrade weapon
    public bool TryUpgradeWeapon(){
        
        if(weaponPrices.Count <= weapon.weaponLevel){//is the weapon max level?
            return false;
        }
        if(gold >= weaponPrices[weapon.weaponLevel]){
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }


    //EXPERIENCE System

    public int GetCurrentLevel(){

        int r =0;   //return value
        int add =0; 

        while(experience >=add){    //loop through our levels and return the level that it gets back
            add+=xpTable[r];
            r++;
        }
        return r;
        if(r == xpTable.Count){     // if we are maxLevel
            return r;
        }
    }

    public int GetXpToLevel(int level){

        int r =0;
        int xp=0;
        while(r < level){
            xp+=xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp){
        int currLevel = GetCurrentLevel();
        experience += xp;
        if(currLevel<GetCurrentLevel()){
            OnLevelUp();
        }
    }
    public void OnLevelUp(){
        player.OnLevelUp();
    }

    //Save state of game
    /*
    * INT preferredSkin
    * INT gold
    * INT experience
    * INT weaponLevel
    */
    public void SaveState(){

        string s = "";

        s += "0" + "|";                // placeholder for skin
        s += gold.ToString() +"|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();                      //place holder for weapon level

        PlayerPrefs.SetString("SaveState", s);// this takes the value of s(which should be a long string with lots of |'s and then call it: SaveState)
        UnityEngine.Debug.Log("Saved State confirmed");
    }
    public void LoadState(Scene s, LoadSceneMode mode){// To be honest Im not 100% sure what is going on here but it is needed to get string s

        if(!PlayerPrefs.HasKey("SaveState")){
            return;//If there has been no SaveState yet,like at the start of a run, we will not get an error.
            UnityEngine.Debug.Log("Did not Find key SaveState");
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');//This Gets our SaveState string and then, use ' ' for it here, This will allow us to split the values of the string on |
                                                                    //Example SaveString: "0|10|150|0" >>   "0"
                                                                    //                                      "10"
                                                                    //                                      "150"
                                                                    //                                      "0"
                                                                    //this shows: preferredSkin|gold|experience|weaponValue
        //Change Player Skin
            //we current leave this blank because we have no skins
        //Current Gold
        gold = int.Parse(data[1]);// this will convert our String at position [1] to an int
        //Current Experience
        experience = int.Parse(data[2]);// this will convert our String at position [1] to an int
        if(GetCurrentLevel() != 1){
            player.SetLevel(GetCurrentLevel());
        }
        //Current Level
        player.SetLevel(GetCurrentLevel());
        //Players Current Weapon Level
        weapon.SetWeaponLevel(int.Parse(data[3]));
        
                    //weapon.SetWeaponLevel(int.Parse(data[3]));
            //we current leave this blank because we have no weapon levels yet
        // sets spawn point to our spawn point within the scene

        // SceneManager.sceneLoaded -=LoadState;
        Debug.Log("SaveState was found" + gold + experience);
    }               

}
