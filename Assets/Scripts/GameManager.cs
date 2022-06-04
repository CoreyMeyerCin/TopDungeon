using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;//by making this static EVERY other class in our code may call GameManager without GetComponent<GameManager>(); use GameManager.instance
    private void Awake(){

        if(GameManager.instance != null){//If you already have a GameManager destroy the gameManager that exists inside the scene already
            Destroy(gameObject);
            return;
        }

        instance=this;//assign the instance to ourself
        SceneManager.sceneLoaded+= LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources for the game
    public List<Sprite> playerSprites;
    public List <Sprite> weaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
        //public Weapon weapon; etc..

    //Logic
    public int gold;
    public int experience;

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
        s += "0";                      //place holder for weapon level



        PlayerPrefs.SetString("SaveState", s);// this takes the value of s(which should be a long string with lots of |'s and then call it: SaveState)
    }
    public void LoadState(Scene s, LoadSceneMode mode){// To be honest Im not 100% sure what is going on here but it is needed to get string s

        if(!PlayerPrefs.HasKey("SaveState")){
            return;//If there has been no SaveState yet,like at the start of a run, we will not get an error.
            Debug.Log("Did not Find key SaveState");
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
        //Players Current Weapon Level
            //we current leave this blank because we have no weapon levels yet
            Debug.Log("SaveState was found" + gold + experience);
    }               

}
