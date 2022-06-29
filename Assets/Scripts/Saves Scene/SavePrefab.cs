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
        thisTexture.name = picPath;
        _rawImage.texture = thisTexture;
        SizeToParent();
    }

    public void LoadGame()
    {
        _gameStateManager.LoadGame(Path);

    }
    public void SizeToParent(float padding = 0)
    {
        var parent = _rawImage.transform.parent.GetComponent<RectTransform>();
        var imageTransform = _rawImage.GetComponent<RectTransform>();
        if (!parent) { return; } //if we don't have a parent, just return our current width;
        padding = 1 - padding;
        float w = 0, h = 0;
        float ratio = _rawImage.texture.width / (float)_rawImage.texture.height;
        var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
        if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
        {
            //Invert the bounds if the image is rotated
            bounds.size = new Vector2(bounds.height, bounds.width);
        }
        //Size by height first
        h = bounds.height * padding;
        w = h * ratio;
        if (w > bounds.width * padding)
        { //If it doesn't fit, fallback to width;
            w = bounds.width * padding;
            h = w / ratio;
        }
        imageTransform.sizeDelta = new Vector2(w, h);
    }
}
