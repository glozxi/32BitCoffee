using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager instance;
    public static string globalPath = "Images/BG/";

    Dictionary<string, string> bgManger = new Dictionary<string, string>()
    {
        {"day", globalPath+"ruben-ramirez-xhKG01FN2uk-unsplash (1)" },
        {"evening", globalPath+"AnatolyShabanWonderFair" },
        {"night", globalPath+"normal" },
        {"rain_day", globalPath+"normal" }
    };
    private void Awake()
    {
        instance = this;
    }
    public void CMChangeBackground(string userInput)
    {
        Image bg = gameObject.GetComponent<Image>();
        bg.sprite = Resources.Load<Sprite>(bgManger[userInput]);
    }
}
