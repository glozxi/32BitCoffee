using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class SavePrefab : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private GameStateManager _gameStateManager;
    [SerializeField]
    private RawImage _rawImage;
    private SaveData _saveData;

    // *.png
    private string _picPath;
    // Path is *.save
    private string _path;
    public string Path
    {
        get => _path;
        set
        {
            _path = value;
            _picPath = Path.Split('.')[0] + ".png";
            DeserializeData();
            SetImage();
            SetText();
        }
    }



    private void SetText()
    {
        string text = $"{_saveData.Time}\n" +
            $"Day {_saveData.Day}\n" +
            $"Cash: {_saveData.Cash}\n" +
            $"NPoints: {_saveData.NetworkPoints}";
        _text.text = text;
    }

    private void SetImage()
    {
        Texture2D thisTexture = new Texture2D(1, 1);
        byte[] bytes = File.ReadAllBytes(_picPath);
        thisTexture.LoadImage(bytes);
        _rawImage.texture = thisTexture;
    }

    private void DeserializeData()
    {
        _saveData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(_path));
    }

    public void DeleteSave()
    {
        File.Delete(_picPath);
        File.Delete(_path);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadGame()
    {
        _gameStateManager.LoadGame(_saveData);
    }


}