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
            pageSourceFetcher.GetLink(InputField.text,OnGetLink,OnGetLinkFail);
            PrintLog( "fetching data...",Color.yellow);
        }
        else if (InputField.text == "")
        {
            PrintLog("",Color.white);
        }
        else if (!InputField.text.Contains("http"))
        {
            downloadText.text ="Invalid Link";
            PrintLog( "Invalid Link",Color.red);
        }
        else if(!InputField.text.Contains("html"))
        {
            downloadText.text ="Invalid Link";
            PrintLog( "Invalid Link",Color.red);
        }
    }
    public void PrintLog(string msg,Color color)
    {
        print(msg);
        logTxt.color = color;
        logTxt.text=msg;
    }
    public void OnGetLink(string link)
    {
        PrintLog( "File Ready", Color.green);
        downloadableLink = link;
        PrintLog(downloadableLink, Color.green);
        downloadButton.interactable = true;
        //Application.OpenURL(downloadableLink);
        Open(link);
        downloadText.text ="Opened";
    }
    public void OnGetLinkFail(string error)
    {
        PrintLog( "Error: " + error, Color.red);
    }
    public void Open(string url)
    {
        // #if UNITY_WEBGL && !UNITY_EDITOR
        // Application.ExternalEval("window.open('" + url + "', '_blank')");
        // //Application.ExternalEval("OpenURL.open('" + url + "');");
        // #else
        Application.OpenURL(url); // Fallback for non-WebGL platforms
       // #endif
    }
    
}
