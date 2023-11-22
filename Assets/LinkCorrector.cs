using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LinkCorrector : MonoBehaviour
{
    public string downloadableLink;
    public TMP_InputField InputField;
    public PageSourceFetcher pageSourceFetcher;
    public TMP_Text logTxt;
    public Button downloadButton;
    public void DownloadButton()
    {

            Application.OpenURL(downloadableLink);
        
    }

    private void Start()
    {
        downloadButton.interactable = false;
    }
    public void OnValueChangeField()
    {
        downloadButton.interactable = false;
        if (InputField.text == "")
        {
            logTxt.text = "";
        }
        
        else if (!InputField.text.Contains("http"))
        {
            logTxt.color=Color.red;
            logTxt.text = "Invalid Link";
        }
        else if(!InputField.text.Contains("html"))
        {
            logTxt.color=Color.red;
            logTxt.text = "Invalid Link";
        }
        else
        {
            pageSourceFetcher.GetLink(InputField.text,OnGetLink);
            logTxt.color=Color.yellow;
            logTxt.text = "fetching data...";
        }
    }

    public void OnGetLink(string link)
    {
        logTxt.color=Color.green;
        logTxt.text = "File Ready";
        downloadableLink = link;
        downloadButton.interactable = true;
    }
}
