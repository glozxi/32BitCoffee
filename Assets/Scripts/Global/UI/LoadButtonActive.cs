using System.IO;
using UnityEngine;

public class LoadButtonActive : MonoBehaviour
{
    [SerializeField]
    private GameObject _button;

    private void OnEnable()
    {
        string savePath = Application.persistentDataPath + "/savedata.save";
        _button.SetActive(File.Exists(savePath));
    }
}
