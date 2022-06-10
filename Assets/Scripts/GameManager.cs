using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance; //by making this static EVERY other class in our code may call GameManager without GetComponent<GameManager>(); use GameManager.instance

    // using strings to help with the parsing so that we don't have to rely on keeping indexes the same
    // this will work regardless of the order that the save string is built in, and it is very scalable because of that.
    // I've set these up here so they're easily changeable without having to alter code
    private static readonly string SAVE_STRING_PARAM_DELIMETER = "|"; // separates each individual key-value pair (gold + gold_amount),(exp + exp_amount)
    private static readonly string SAVE_STRING_KEY_VALUE_DELIMETER = "~"; // separates key from value (gold),(gold_amount)
    private static readonly string SAVE_FILENAME = "save.txt";

    private static readonly string SKIN_SAVE_STRING_KEY = "SKIN_SAVE";
    private static readonly string GOLD_SAVE_STRING_KEY = "GOLD_SAVE";
    private static readonly string EXPERIENCE_SAVE_STRING_KEY = "EXP_SAVE";
    private static readonly string WEAPON_LEVEL_SAVE_STRING_KEY = "WEP_LVL_SAVE";



    private void Awake()
    {
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
            SceneManager.sceneLoaded += LoadState;
        }
        else
        {
            Debug.Log("Destroying extra GameManager");
            Destroy(this.gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
        }

        //instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices = new List<int> { 100, 200, 300, 400, 500, 600, 700, 800, 900 };
    public List<int> xpTable = new List<int> { 3, 7, 15, 25, 40, 58, 70, 95, 130, 170 };

    //References
    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    //Logic
    public int gold;
    public int experience;

    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration); //add here so it can be called from static GameManager
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {    
        if(weaponPrices.Count <= weapon.weaponLevel) //is the weapon max level?
        {
            return false;
        }

        if(gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }


    //************************************************
    //EXPERIENCE SYSTEM
    //************************************************
    public int GetCurrentLevel()
    {
        int r = 0;   //return value
        int add = 0; 

        while(experience >= add) //loop through our levels and return the level that it gets back
        {    
            add+=xpTable[r];
            r++;
        }
        return r;

        if(r == xpTable.Count) // if we are maxLevel
        {     
            return r;
        }
    }

    public int GetXpToLevel(int level)
    {
        int r =0;
        int xp=0;
        while(r < level)
        {
            xp+=xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;

        if(currLevel<GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
    }


    //************************************************
    //PERSIST SAVE DATA AND LOAD SAVE DATA 
    //************************************************
    public void SaveState()
    {
        List<string> saveParams = new List<string>(); // this list gets passed in to AssembleSaveString() which creates the final string
        saveParams.Add(CreateSaveStringKvp(SKIN_SAVE_STRING_KEY, "skin_placeholder"));
        saveParams.Add(CreateSaveStringKvp(GOLD_SAVE_STRING_KEY, gold.ToString()));
        saveParams.Add(CreateSaveStringKvp(EXPERIENCE_SAVE_STRING_KEY, experience.ToString()));
        saveParams.Add(CreateSaveStringKvp(WEAPON_LEVEL_SAVE_STRING_KEY, weapon.weaponLevel.ToString()));

        var saveStateString = AssembleSaveString(saveParams);

        WriteSaveData(saveStateString);
    }

    private string CreateSaveStringKvp(string key, string value)
	{
        var saveStringKvp = key + SAVE_STRING_KEY_VALUE_DELIMETER + value;
        return saveStringKvp;
	}

    private string AssembleSaveString(List<string> KeyValuePairs) // concatenate all passed in parameters into a single string
	{
        StringBuilder s = new StringBuilder();
        for(int i = 0; i < KeyValuePairs.Count; i++)
		{
            if(i == KeyValuePairs.Count - 1)
			{
                s.Append(KeyValuePairs[i]); // do not append delimeter after final member
            }
			else
			{
                s.Append($"{KeyValuePairs[i]}{SAVE_STRING_PARAM_DELIMETER}");
            }
		}

        return s.ToString();
    }

	private void WriteSaveData(string saveString) //creates save file if doesn't exist already, writes to if does
	{
        string docPath = Directory.GetCurrentDirectory();
        Debug.Log($"writing to save file at {docPath}");
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, SAVE_FILENAME)))
        {
            outputFile.WriteLine(saveString);
        }
    }

    public string ReadSaveData()
	{
        var currentDirectory = Directory.GetCurrentDirectory();
        var filepath = $"{currentDirectory}\\{SAVE_FILENAME}";
        if (File.Exists(filepath))
        {
            Debug.Log("Save file found, reading...");
            string[] lines = File.ReadAllLines(filepath);

            Debug.Log($"{lines.Length} save strings found, loading last"); //only sending last line atm. This will become more sophisticated as the system grows.
            if (lines.Length > 0)
			{
                return lines.Last();
			}
			else
			{
                throw new Exception("Error: save file is empty!");
			}
        }
        else
        {
            throw new Exception("Error: save file not found!");
        }
    }

    public void LoadState(Scene s, LoadSceneMode mode)
	{
        var saveString = ReadSaveData();
        //commenting out as we should only be loading at the start as the data is already persisted in gamemanager once loaded
        //if(!PlayerPrefs.HasKey("SaveState"))
        //{ 
        //    Debug.Log("Did not Find key SaveState,"); //If there has been no SaveState yet, like at the start of a run, we will not get an error.
        //    return;
        //}
 

        // split the save string twice to create a dictionary, first on | for  each key-value pair, then on ~ to separate each key + value. Visual example:
        // "EXP~4|WEP_LVL~200|GOLD~I'm poor please help" becomes a Dictionary<string, string> =>
        // Key: "EXP",      Value: "4"
        // Key: "WEP_LVL",  Value: "200"
        // Key: "GOLD",     Value: "I'm poor please help"
        var saveDataDict = saveString.Split(new[] { SAVE_STRING_PARAM_DELIMETER }, StringSplitOptions.RemoveEmptyEntries)
             .Select(x => x.Split(SAVE_STRING_KEY_VALUE_DELIMETER))
             .ToDictionary(x => x[0], y => y[1]);

        var entriesMissingData = saveDataDict.Select(x => x).Where(x => string.IsNullOrEmpty(x.Value)); // check for missing save data values
        if (entriesMissingData.Any())
        {
            var missingValues = string.Join(",", entriesMissingData.Select(x => x.Key));
            Debug.Log($"Save data missing values: {missingValues}. Exiting");
            return;
        }

        gold = int.Parse(saveDataDict[GOLD_SAVE_STRING_KEY]);
        experience = int.Parse(saveDataDict[EXPERIENCE_SAVE_STRING_KEY]);
        weapon.SetWeaponLevel(int.Parse(saveDataDict[WEAPON_LEVEL_SAVE_STRING_KEY])); //we currently leave this blank because we have no weapon levels yet

        if (GetCurrentLevel() != 1) //what is this block doing? either way the same method gets called
        {
            player.SetLevel(GetCurrentLevel());
        }
        player.SetLevel(GetCurrentLevel());

        // sets spawn point to our spawn point within the scene
        SceneManager.sceneLoaded -= LoadState;
        Debug.Log($"SaveState was found - Gold: {gold}, Exp: {experience}");
    }

}
