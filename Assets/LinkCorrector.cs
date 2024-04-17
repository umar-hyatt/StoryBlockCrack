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
    public TMP_Text downloadText;
    public void DownloadButton()
    {
        ProvidedLinkChecker();
    }

    public void OnLinkChange()
    {
        downloadText.text ="Download";
    }
    public void ProvidedLinkChecker()
    {
        //downloadButton.interactable = false;
        if(InputField.text.Contains("html")||InputField.text.Contains("item"))
        {
            downloadText.text ="Downloading...";
            pageSourceFetcher.GetLink(InputField.text,OnGetLink);
            logTxt.color=Color.yellow;
            PrintLog( "fetching data...");
        }
        else if (InputField.text == "")
        {
            PrintLog("");
        }
        else if (!InputField.text.Contains("http"))
        {
            downloadText.text ="Invalid Link";
            logTxt.color=Color.red;
            PrintLog( "Invalid Link");
        }
        else if(!InputField.text.Contains("html"))
        {
            downloadText.text ="Invalid Link";
            logTxt.color=Color.red;
            PrintLog( "Invalid Link");
        }
    }
    public void PrintLog(string msg)
    {
        print(msg);
        logTxt.text=msg;
    }
    public void OnGetLink(string link)
    {
        logTxt.color=Color.green;
        PrintLog( "File Ready");
        downloadableLink = link;
        downloadButton.interactable = true;
        Application.OpenURL(downloadableLink);
        downloadText.text ="Opened";
    }
}
