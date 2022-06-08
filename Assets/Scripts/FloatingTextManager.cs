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
    private void Update(){
        foreach(FloatingText txt in floatingTexts){
            txt.UpdateFloatingTexts();//this updates every frame
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){

        FloatingText floatingText = GetFloatingText();

        floatingText.text.text = msg;//manually changing the message
        floatingText.text.fontSize = fontSize;
        floatingText.text.color = color;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position) ;//we have to do it like this because UI is in screenSpace not worldSpace,
                                                                                    //So it takes the position when it appears and converts that to ScreenSpace
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }
    private FloatingText GetFloatingText(){

        FloatingText txt = floatingTexts.Find(t=> !t.active);// looks for floatingTexts that is not active

        if (txt==null){
            
            txt= new FloatingText();
            txt.go= Instantiate(textPrefab);// creating a new GameObject and assigning it to txt.go
            txt.go.transform.SetParent(textContainer.transform);
            txt.text = txt.go.GetComponentInParent<Text>();// this sets txt.text as go.text

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
