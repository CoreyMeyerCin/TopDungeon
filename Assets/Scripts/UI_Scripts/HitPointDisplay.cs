using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointDisplay : MonoBehaviour
{
    private float hitPoints;
    private float maxHitPoints;
    public Text healthText;
    private Canvas canvas;

    private void Start()
    {
        canvas=this.GetComponentInParent<Canvas>();
        canvas.enabled = false;
    }
    void OnGUI()
    {
        hitPoints = GameManager.instance.player.hitpoints;
        maxHitPoints = GameManager.instance.player.maxHitpoints;
        healthText.text = $"HP:{hitPoints}/{maxHitPoints}";

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvas.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            canvas.enabled = false;
        }
        
    }
}
