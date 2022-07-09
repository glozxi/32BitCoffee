using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

// TODO: Separate into new classes
public class GameStateManager : MonoBehaviour
{
    private InkManager _inkManager;
    private Points _points;

    // Start is called before the first frame update
    void Start()
    {
        _inkManager = FindObjectOfType<InkManager>();
        _points = FindObjectOfType<Points>();
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
    }

    public void StartNewGame()
    {
        InkManager.ResetStory();
        _points.ResetPoints();
        StartGame();
    }

    // Save from VN scene
    public void SaveGame()
    {
        SaveData saveData = new()
        {
            InkStoryState = _inkManager.GetStoryState(),
            TextLog = _inkManager.GetTextLog(), //This may cause issues in future for TextLog, also this loads the entire story.
            Cash = _points.Cash,
            NetworkPoints = _points.NetworkPoints,
            NextBrewLevel = State.Instance.NextBrewLevel,
            Outcome = State.Instance.Outcome,
            Drink = State.Instance.Drink,
            BGMFile = State.Instance.BGMFile,
            Time = DateTime.Now,
            CharDatas = CharacterManager.instance.GetEnabledCharDatas(),
            ActiveUpgrades = UpgradesData.GetStrFromUpgrades(State.Instance.Upgrades)
        };

        BinaryFormatter bf = new();
        int i = 0;
        for (; File.Exists(Application.persistentDataPath + "/savedata" + i + ".save"); i++) { }
        string savePath = Application.persistentDataPath + "/savedata" + i + ".save";
        string picSavePath = Application.persistentDataPath + "/savedata" + i + ".png";

        FileStream file = File.Create(savePath);
        bf.Serialize(file, saveData);

        file.Close();
        ScreenshotNow _screenshotter = FindObjectOfType<ScreenshotNow>();
        _screenshotter.Screenshot(picSavePath);

        print("Game saved at " + savePath);

    }

    // Load from start screen scene
    public void LoadGame(SaveData saveData)
    {
        InkManager.LoadState(saveData.InkStoryState, saveData.TextLog);
        _points.LoadPoints(saveData.Cash, saveData.NetworkPoints);

        State.Instance.NextBrewLevel = saveData.NextBrewLevel;
        State.Instance.Outcome = saveData.Outcome;
        State.Instance.Drink = saveData.Drink;
        State.Instance.BGMFile = saveData.BGMFile;
        State.Instance.CharDatas = saveData.CharDatas;
        State.Instance.Upgrades = UpgradesData.GetUpgradesFromStr(saveData.ActiveUpgrades);

        StartGame();


    }

    // To rename with ExitGamePrompt, and create new one which does the actual exit
    public void ExitGame()
    {
        // TODO: Display "do you want to quit?"
        Application.Quit();
    }


}