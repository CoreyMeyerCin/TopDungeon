using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public bool isActive;
    public GameObject gameObj;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        isActive = true;
        lastShown = Time.time;
        gameObj.SetActive(true);
    }

    public void Hide()
    {
        isActive = false;
        gameObj.SetActive(false);
    }

    public void UpdateFloatingTexts()
    {
        gameObj.transform.position += motion * Time.deltaTime; // update the text in direction motion
		if (!isActive)
		{
			return;
		}

		if (Time.time - lastShown > duration) // check how long something has existed. becomes false when duration is bigger than how long its been shown
        {
            Hide();
        }
    }
}
