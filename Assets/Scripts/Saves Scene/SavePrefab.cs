using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class SavePrefab : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private GameStateManager _gameStateManager;
    [SerializeField]
    private RawImage _rawImage;

    // Path with .save
    public string Path
    { get; set; }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetImage()
    {
        Texture2D thisTexture = new Texture2D(1, 1);
        string picPath = Path.Split('.')[0] + ".png";
        byte[] bytes = File.ReadAllBytes(picPath);
        thisTexture.LoadImage(bytes);
        _rawImage.texture = thisTexture;
    }

    public void LoadGame()
    {
        _gameStateManager.LoadGame(Path);

    }
    
}
