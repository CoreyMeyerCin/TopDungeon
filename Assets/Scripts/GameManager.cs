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

    private static readonly string PLAYER_LEVEL = "PLAYER_LEVEL";
    private static readonly string SKIN_SAVE_STRING_KEY = "SKIN_SAVE";
    private static readonly string GOLD_SAVE_STRING_KEY = "GOLD_SAVE";
    private static readonly string EXPERIENCE_SAVE_STRING_KEY = "EXP_SAVE";
    //private static readonly string WEAPON_LEVEL_SAVE_STRING_KEY = "WEP_LVL_SAVE";

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrices = new List<int> { 100, 200, 300, 400, 500, 600, 700, 800, 900 };

    //References
    public Player player;
    public Weapon weapon;
    public HealthService healthService;

    public FloatingTextManager floatingTextManager;
    public ExperienceManager experienceManager;
    public LevelUI levelUI;

    private void Awake()
    {
        InstantiateController();
    }

    private void InstantiateController()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(player.gameObject);
            DontDestroyOnLoad(floatingTextManager.gameObject);
            DontDestroyOnLoad(experienceManager.gameObject);
            DontDestroyOnLoad(levelUI.gameObject);
            DontDestroyOnLoad(healthService);
            DontDestroyOnLoad(weapon);
            SceneManager.sceneLoaded += LoadState;
        }
        else
        {
            Debug.Log("Destroying extra GameManager");
            Destroy(this.gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(levelUI.gameObject);
            Destroy(experienceManager.gameObject);
            Destroy(healthService.gameObject);
            Destroy(weapon);
            SceneManager.sceneLoaded += LoadState;
        }

        //instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    


    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration); //add here so it can be called from static GameManager
    }

    //Upgrade weapon
    //public bool TryUpgradeWeapon()
    //{    
    //    if(weaponPrices.Count <= weapon.weaponLevel) //is the weapon max level?
    //    {
    //        return false;
    //    }

    //    if(gold >= weaponPrices[weapon.weaponLevel])
    //    {
    //        gold -= weaponPrices[weapon.weaponLevel];
    //        weapon.UpgradeWeapon();
    //        return true;
    //    }
    //    return false;
    //}


    //************************************************
    //PERSIST SAVE DATA AND LOAD SAVE DATA 
    //************************************************
    public void SaveState()
    {
        List<string> saveParams = new List<string>(); // this list gets passed in to AssembleSaveString() which creates the final string
        saveParams.Add(CreateSaveStringKvp(PLAYER_LEVEL, player.GetLevel().ToString()));
        saveParams.Add(CreateSaveStringKvp(SKIN_SAVE_STRING_KEY, "skin_placeholder_value")); //TODO: add actual value source
        saveParams.Add(CreateSaveStringKvp(GOLD_SAVE_STRING_KEY, player.gold.ToString()));
        saveParams.Add(CreateSaveStringKvp(EXPERIENCE_SAVE_STRING_KEY, ExperienceManager.instance.GetExperience().ToString()));
        //saveParams.Add(CreateSaveStringKvp(WEAPON_LEVEL_SAVE_STRING_KEY, weapon.weaponLevel.ToString()));

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

            Debug.Log($"{lines.Length} save strings found, loading last"); //only returning last line atm. This will become more sophisticated as the system grows.
            if (lines.Length > 0)
			{
                return lines.Last();
			}
			else
			{
                Debug.Log("Error: save file is empty!");
			}
        }
        else
        {
            Debug.Log("Error: save file not found!");
        }

        return string.Empty;
    }

    public void LoadState(Scene s, LoadSceneMode mode)
	{
        var saveString = ReadSaveData();

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

        Player.instance.SetLevel(int.Parse(saveDataDict[PLAYER_LEVEL]));
        player.gold = int.Parse(saveDataDict[GOLD_SAVE_STRING_KEY]);
        ExperienceManager.instance.SetExperience(int.Parse(saveDataDict[EXPERIENCE_SAVE_STRING_KEY]));
        //weapon.SetWeaponLevel(int.Parse(saveDataDict[WEAPON_LEVEL_SAVE_STRING_KEY])); //we currently leave this blank because we have no weapon levels yet

        SceneManager.sceneLoaded -= LoadState;
        Debug.Log($"SaveState was found - Level {Player.instance.GetLevel()} Gold: {player.gold}, Exp: {ExperienceManager.instance.GetExperience()}");
    }

}
