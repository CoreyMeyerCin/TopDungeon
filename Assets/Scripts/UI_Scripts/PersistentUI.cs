using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentUI : MonoBehaviour
{
    private float hitPoints;
    private float maxHitPoints;
    public Text healthText;

    //public RectTransform xpBar;
    public float currentExp;
    public float maxExp;
    public Text expText;

    public Text levelText;

    //public Text goldText;

    private Canvas canvas;
    private static bool showGui;


    //private void Start()
    //{
    //    showGui = false;
    //    canvas=this.GetComponent<Canvas>();
    //}
    //void OnGUI()
    void Update()
    {
        //if (showGui) { canvas.enabled = true; } else { canvas.enabled = false; }
        hitPoints = Player.instance.stats.hitpoints;
        maxHitPoints = Player.instance.stats.maxHitpoints;
        //currentExp = (float)GameManager.instance.experienceManager.GetExperience();
        //maxExp = (float)GameManager.instance.experienceManager.GetExperienceToNextLevel();
        healthText.text = $"HP:{hitPoints}/{maxHitPoints}";
        expText.text = $"EXP:{GameManager.instance.experienceManager.GetExperience()}/{GameManager.instance.experienceManager.GetExperienceToNextLevel()}";
        //goldText.text = $"Gold:{Player.instance.gold}";
        levelText.text = $"Level: {Player.instance.stats.level}";

        //if (Input.GetKeyDown(KeyCode.Tab) && !showGui)
        //{
        //    showGui = true;
        //}
        //else if (Input.GetKeyUp(KeyCode.Tab) && showGui)
        //{
        //    showGui = false;
        //}


    }
  
}
