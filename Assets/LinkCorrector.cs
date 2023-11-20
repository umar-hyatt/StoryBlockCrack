using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LinkCorrector : MonoBehaviour
{
    public TMP_InputField InputField;
    public TMP_Text logTxt;
    public void DownloadButton()
    {
        if (InputField.text.Contains(".mp3"))
        {
            string currentLink = InputField.text;
            var correctLink = currentLink.Replace("\\", "");
            Application.OpenURL(correctLink);
        }
    }

    public void OnValueChangeField()
    {
        if (InputField.text == "")
        {
            logTxt.text = "";
        }
        
        else if (!InputField.text.Contains("http"))
        {
            logTxt.color=Color.red;
            logTxt.text = "Invalid Link";
        }
        else if(!InputField.text.Contains(".mp3"))
        {
            logTxt.color=Color.red;
            logTxt.text = "Invalid Link";
        }
        else
        {
            logTxt.color=Color.green;
            logTxt.text = "Valid Link";
        }
        
    }
}
