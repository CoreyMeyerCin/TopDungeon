using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    //public delegate void ExperienceChangedEvent(int exp);
    //public static event ExperienceChangedEvent experienceChangedEvent;
    //public delegate void LevelChangedEvent();
    //public static event LevelChangedEvent levelChangedEvent;

    private int experience;
    private int experienceToNextLevel = 100;
    //private List<int> expTable = new List<int> { 3, 7, 15, 25, 40, 58, 70, 95, 130, 170 };

	private void Awake()
	{
        if(instance == null)
		{
            instance = this;
		}
		//experienceChangedEvent += OnExperienceChanged;
        //levelChangedEvent += OnLevelChanged;
	}

	private void OnEnable()
	{
        EventManager.StartListening(EventManager.EventType.experienceChangedEvent, OnExperienceChanged);
        EventManager.StartListening(EventManager.EventType.levelChangedEvent, OnLevelChanged);
	}

	public void AddExp(int exp)
    {
        //experienceChangedEvent?.Invoke(exp);
        EventManager.TriggerEvent(EventManager.EventType.experienceChangedEvent, new Dictionary<string, object> { { "exp", exp } } );
    }

    private void CalculateNewExperienceToNextLevel()
	{
        var next = (int)Math.Round(experienceToNextLevel * 1.17);
        experienceToNextLevel = next;
	}

    public int CalculateScaledExperienceValue(int baseExp) //adjust experience values based on level, etc.
	{
        //TODO: add exp scaling logic
        var scaledExp = baseExp;
        return scaledExp;
	}

    //************************************************
    //EVENT HANDLERS
    //************************************************
    private void OnExperienceChanged(Dictionary<string, object> eventParams)
    {
        Debug.Log("Handling ExperienceChangedEvent...");

        var exp = (int)eventParams["exp"];
        experience += exp;
        while (experience >= experienceToNextLevel)
        {
            CalculateNewExperienceToNextLevel();
            experience -= experienceToNextLevel;
            EventManager.TriggerEvent(EventManager.EventType.levelChangedEvent, null);
            //levelChangedEvent?.Invoke();
        }
    }

    private void OnLevelChanged(Dictionary<string, object> eventParams)
    {
        Debug.Log("Handling LevelChangedEvent...");
        Player.instance.level++;
        //TODO: trigger player level-up animation
    }

    //************************************************
    //ACCESSOR METHODS
    //************************************************
    public float GetExpPercentage()
	{
        return experience / experienceToNextLevel;
	}

    public int GetExperience()
	{
        return experience;
	}

    public void SetExperience(int experienceToSet)
	{
        experience = experienceToSet;
	}

    public int GetExperienceToNextLevel()
	{
        return experienceToNextLevel;
	}

    public void SetExperienceToNextLevel(int experienceToSet)
	{
        experienceToNextLevel = experienceToSet;
	}

}
