using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSetText : MonoBehaviour
{
    public Text uiText;

    public void Update()
    {
        GetComponent<Text>().text = uiText.text;
    }
}
