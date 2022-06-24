using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistantUI : MonoBehaviour
{
    private float hitPoints;
    private float maxHitPoints;
    public Text healthText;

    public RectTransform xpBar;
    public float currentExp;
    public float maxExp;
    public Text expText;

    public Text goldText;

    private Canvas canvas;
    private static bool showGui;


    private void Start()
    {
        showGui = false;
        canvas=this.GetComponent<Canvas>();
    }
    void OnGUI()
    {
        if (showGui) { canvas.enabled = true; } else { canvas.enabled = false; }
        

        hitPoints = GameManager.instance.player.hitpoints;
        maxHitPoints = GameManager.instance.player.maxHitpoints;
        //currentExp = (float)GameManager.instance.experienceManager.GetExperience();
        //maxExp = (float)GameManager.instance.experienceManager.GetExperienceToNextLevel();
        healthText.text = $"HP:{hitPoints}/{maxHitPoints}";
        expText.text = $"EXP:{GameManager.instance.experienceManager.GetExperience()}/{GameManager.instance.experienceManager.GetExperienceToNextLevel()}";
        goldText.text = $"Gold:{GameManager.instance.player.gold}";

        if (Input.GetKeyDown(KeyCode.Tab) && !showGui)
        {
            showGui = true;
        }
        else if (Input.GetKeyUp(KeyCode.Tab) && showGui)
        {
            showGui = false;
        }


    }
  
}
