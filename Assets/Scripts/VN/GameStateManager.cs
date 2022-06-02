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

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DialogueScene");
    }
    public void SaveGame()
    {
        SaveData saveData = new() { InkStoryState = _inkManager.GetStoryState() };
        BinaryFormatter bf = new();
        string savePath = Application.persistentDataPath + "/savedata.save";
        FileStream file = File.Create(savePath);

        bf.Serialize(file, saveData);

        file.Close();
        print("Game saved at " + savePath);

    }
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

            _inkManager.LoadState(saveData.InkStoryState);
        }
    }

    public void ExitGame()
    {
        // TODO: Display "do you want to quit?"
        Application.Quit();
    }


}
