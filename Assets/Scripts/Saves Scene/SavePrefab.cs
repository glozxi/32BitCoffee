using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavePrefab : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private GameStateManager _gameStateManager;

    public string Path
    { get; set; }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void LoadGame()
    {
        // Application.persistentDataPath + "/savedata.save";
        _gameStateManager.LoadGame(Path);

    }
}
