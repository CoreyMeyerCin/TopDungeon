using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool active; //is it on or off
    public GameObject go; //reference GameObject
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time; //Time.time means current time
        go.SetActive(true);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(false);
    }

    public void UpdateFloatingTexts()
    {
    go.transform.position += motion * Time.deltaTime; // update the text in direction motion
        if(!active)
        {
            return;
        }
        if(Time.time - lastShown > duration) // check how long something has existed. becomes false when duration is bigger than how long its been shown
        {
            Hide();
        }
    }
}
