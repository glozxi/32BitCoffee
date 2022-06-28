using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System.Linq;

public class Saves : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _rows;

    [SerializeField]
    private GameObject _savePrefab;

    [SerializeField]
    private GameObject _prev;
    [SerializeField]
    private GameObject _next;
    
    private int MAX_SAVE_IN_ROW = 2;
    private int MAX_IN_PAGE;

    private int _firstShown;
    public int FirstShown
    { get => _firstShown; }

    string[] filePaths;


    // Start is called before the first frame update
    void Start()
    {
        _firstShown = 0;
        MAX_IN_PAGE = _rows.Length * MAX_SAVE_IN_ROW;
        filePaths = Directory.GetFiles(Application.persistentDataPath + "/", "*.save");
        RemoveSavesObjects();
        PrevAndNextButtonsActive();
        InstantiateSavePrefabs(filePaths, _firstShown);    
    }

    private void InstantiateSavePrefabs(string[] filePaths, int firstShown)
    {
        int i = firstShown;
        foreach (GameObject row in _rows)
        {
            for (int j = 0; j < MAX_SAVE_IN_ROW; j++)
            {
                if (i == filePaths.Length)
                {
                    return;
                }
                GameObject obj = Instantiate(_savePrefab, row.transform);
                obj.GetComponent<SavePrefab>().SetText(
                    filePaths[i].Split("/").Last().Split('.')[0]);
                obj.GetComponent<SavePrefab>().Path = filePaths[i];
                i++;
            }
        }
    }

    private void RemoveSavesObjects()
    {
        foreach (GameObject row in _rows)
        {
            foreach (Transform transform in row.transform)
            {
                Destroy(transform.gameObject);
            }
            
        }
    }

    public void NextPage()
    {
        if (_firstShown + MAX_IN_PAGE >= filePaths.Length)
        {
            print(_firstShown);
            print(MAX_IN_PAGE);
            print(filePaths.Length);
            Debug.LogError("No more saves.");
            return;
        }
        RemoveSavesObjects();
        _firstShown += MAX_IN_PAGE;
        PrevAndNextButtonsActive();
        InstantiateSavePrefabs(filePaths, _firstShown);
    }

    public void PrevPage()
    {
        if (_firstShown == 0)
        {
            Debug.LogError("No more saves.");
            return;
        }
        RemoveSavesObjects();
        _firstShown -= MAX_IN_PAGE;
        PrevAndNextButtonsActive();
        InstantiateSavePrefabs(filePaths, _firstShown);
    }

    private void PrevAndNextButtonsActive()
    {
        _prev.SetActive(_firstShown != 0);
        _next.SetActive(_firstShown + MAX_IN_PAGE < filePaths.Length);
        
    }
}
