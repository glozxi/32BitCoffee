using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

// TODO: Separate into new classes
public class GameStateManager : MonoBehaviour
{
    private InkManager _inkManager;

    // Start is called before the first frame update
    void Start()
    {
        _inkManager = FindObjectOfType<InkManager>();
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
    }

    public void StartNewGame()
    {
        InkManager.ResetStory();
        StartGame();
    }

    // Save from VN scene
    public void SaveGame()
    {

        SaveData saveData = new()
        {
            InkStoryState = _inkManager.GetStoryState(),
            TextLog = _inkManager.GetTextLog(), //This may cause issues in future for TextLog, also this loads the entire story.
            Cash = Points.Cash,
            NetworkPoints = Points.NetworkPoints,
            NextBrewLevel = State.NextBrewLevel,
            Outcome = State.Outcome,
            Drink = State.Drink
        };

        BinaryFormatter bf = new();
        string savePath = Application.persistentDataPath + "/savedata.save";
        FileStream file = File.Create(savePath);

        bf.Serialize(file, saveData);

        file.Close();
        print("Game saved at " + savePath);

    }

    // Load from start screen scene
    public void LoadGame()
    {
        string savePath = Application.persistentDataPath + "/savedata.save";
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(savePath, FileMode.Open);

            // Start reading byte sequence from start
            file.Position = 0;
            SaveData saveData = (SaveData)bf.Deserialize(file);
            file.Close();

            InkManager.LoadState(saveData.InkStoryState, saveData.TextLog);
            Points.LoadPoints(saveData.Cash, saveData.NetworkPoints);

            State.NextBrewLevel = saveData.NextBrewLevel;
            State.Outcome = saveData.Outcome;
            State.Drink = saveData.Drink;

            StartGame();
        }
    }

    // To rename with ExitGamePrompt, and create new one which does the actual exit
    public void ExitGame()
    {
        // TODO: Display "do you want to quit?"
        Application.Quit();
    }


}
