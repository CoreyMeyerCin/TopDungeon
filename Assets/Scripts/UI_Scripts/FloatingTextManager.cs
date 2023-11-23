using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start(){
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach(FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingTexts(); //updates every frame
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = msg;
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        floatingText.gameObj.transform.position = Camera.main.WorldToScreenPoint(position) ; // we have to do it like this because UI is in screenSpace not worldSpace,
                                                                                        // So it takes the position when it appears and converts that to ScreenSpace
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.isActive); // looks for floatingTexts that is not active

        if (txt == null)
        {    
            txt = new FloatingText();
            txt.gameObj = Instantiate(textPrefab); // creating a new GameObject and assigning it
            txt.gameObj.transform.SetParent(textContainer.transform);
            txt.text = txt.gameObj.GetComponentInParent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

}
