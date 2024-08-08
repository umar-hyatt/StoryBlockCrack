using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public string storyBlockURL,pond5URL;
    public void StoryBlockButton()
    {
        Application.OpenURL(storyBlockURL);
    }
    public void Pond5Button()
    {
        Application.OpenURL(pond5URL);
    }
}
