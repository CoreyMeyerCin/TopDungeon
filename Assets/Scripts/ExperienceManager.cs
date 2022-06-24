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

    private int experience;
    private int experienceToNextLevel = 100;

	private void Awake()
	{
        if(instance == null)
		{
            instance = this;
		}
	}

	private void OnEnable()
	{
        Actions.OnExperienceChanged += OnExperienceChanged;
	}

	private void OnDisable()
	{
        Actions.OnExperienceChanged -= OnExperienceChanged;
    }

    public void CalculateNewExperienceToNextLevel()
	{
        var next = (int)Math.Round(experienceToNextLevel * 1.17);
        experienceToNextLevel = next;
	}

    public int CalculateScaledExperienceValue(int baseExp) //adjust experience values based on level, etc.
	{
        //TODO: add exp scaling logic for enemies
        var scaledExp = baseExp;
        return scaledExp;
	}

    //************************************************
    //EVENT HANDLERS
    //************************************************
    public void OnExperienceChanged(int exp)
    {
        experience += exp;
        while (experience >= experienceToNextLevel)
        {
            CalculateNewExperienceToNextLevel();
            experience -= experienceToNextLevel;
            Actions.OnLevelUp.Invoke();
        }
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
