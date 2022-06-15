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

    private int level = 1;
    private int experience;
    private int experienceToNextLevel;
    private List<int> expTable = new List<int> { 3, 7, 15, 25, 40, 58, 70, 95, 130, 170 };


    public void AddExp(int exp)
    {
        experience += exp;
        if (experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
        }
    }
    public void OnLevelUp()
    {
        //maxHitpoints++;
        //hitpoints = maxHitpoints;
    }


    public int GetLevel()
	{
        return level;
	}

    public void SetLevel(int levelToSet)
	{
        level = levelToSet;
	}

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

}
