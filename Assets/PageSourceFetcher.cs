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
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("Error: " + webRequest.error);
                onSuccess.Invoke(webRequest.error);
            }
            else
            {
                // Print the page source to the console
                pageSource = webRequest.downloadHandler.text;
                pageSource.Replace("\\", "");
                onSuccess.Invoke(GetCorrectLinks(pageSource));
                Debug.Log("Page Source:\n" + webRequest.downloadHandler.text);
            }
        }
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