using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

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
        _text.text = _saveData.Time.ToString();
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
        BinaryFormatter bf = new();
        FileStream file = File.Open(_path, FileMode.Open);

        // Start reading byte sequence from start
        file.Position = 0;
        _saveData = (SaveData)bf.Deserialize(file);
        file.Close();

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