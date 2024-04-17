using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text.RegularExpressions;

public class PageSourceFetcher : MonoBehaviour
{
    // The URL you want to fetch the page source from
    public string url = "https://www.example.com";
    public string downloadableLink;
    public void GetLink(string _url,Action<string> onsuccess)
    {
        url = _url;
        StartCoroutine(FetchPageSource(onsuccess));
    }

    [TextArea(30, 100)] public string pageSource;
IEnumerator FetchPageSource(Action<string> onSuccess)
{
    //url="https://www.pond5.com/sound-effects/item/52312871-whoosh-opener";
    using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
    {
        Debug.Log("Requesting URL: " + url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + webRequest.error);
            // Call onSuccess with error message or handle it differently
            onSuccess.Invoke("Error occurred: " + webRequest.error);
        }
        else
        {
            string pageSource = webRequest.downloadHandler.text;
            // Assuming GetCorrectLinks is a method you've defined to process links
            if(url.Contains("item"))onSuccess.Invoke(GetCorrectLinksp(pageSource));
            else onSuccess.Invoke(GetCorrectLinks(pageSource));
            Debug.Log("Page Source:\n" + pageSource);
        }
    }
}
string GetCorrectLinksp(string inputString)
{
    // Define the regular expression pattern to match URLs that start with "https://sounds.pond5.com"
    // and end with "_nw_prev.m4a"
    string pattern = @"(https://sounds\.pond5\.com[^\s]*_nw_prev\.m4a)";

    // Use Regex.Matches to find all matches
    MatchCollection matches = Regex.Matches(inputString, pattern);

    // Output the matched URLs
    foreach (Match match in matches)
    {
        string currentUrl = match.Value;
        Debug.Log("Matched URL: " + currentUrl);
        return currentUrl; // Return the first matched URL
    }

    return "Error"; // Return "Error" if no matches are found
}
    string GetCorrectLinks(string inputString)
    {
        //inputString = "Here is a sample string with a .mp3 file: https://example.com/sample.mp3 and another one: https://example.com/another.mp4";

        // Define the regular expression pattern
        string pattern = @"(?<url>https?://[^\s]+\.mp[3])";

        // Use Regex.Match to find the first match
        MatchCollection matches = Regex.Matches(inputString, pattern);

        // Output the matched URLs
        foreach (Match match in matches)
        {
            string currentUrl = match.Groups["url"].Value;
            Debug.Log("Matched URL: " + match.Groups["url"].Value);
            currentUrl= currentUrl.Replace("watermarks", "previews");
             currentUrl=currentUrl.Replace("_WM.mp3", "_NWM.mp3");
             downloadableLink = currentUrl;
             return downloadableLink;
        }

        return "Error";
    }
}