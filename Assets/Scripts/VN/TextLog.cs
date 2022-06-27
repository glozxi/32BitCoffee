using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;

public class TextLog : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _textField;

    [SerializeField]
    private GameObject _content;

    // Currently the text log is stored in a string,
    // but might need to change it.

    public string Log
    { get; set; }

    // prolly need to rethink this entire thing. Queue does not get saved 
    const int CAPACITY = 20;
    public Queue<string> LogQueue = new Queue<string>(CAPACITY);

    private void Awake()
    {
        ChangeTextField();
    }

    private void OnEnable()
    {
        Vector2 newVector = _content.GetComponent<RectTransform>().anchoredPosition;
        newVector.y = 0;
        _content.GetComponent<RectTransform>().anchoredPosition = newVector;
    }

    public void RecordAndChangeTextField(TMP_Text name, TMP_Text dialogue)
    {
        RecordText(name.text, dialogue.text);
        ChangeTextField();
    }

    public void RecordAndChangeTextField(Choice choice)
    {
        RecordText("Your choice", choice.text);
        ChangeTextField();
    }

    private void RecordText(string name, string dialogue)
    {
        string toInsert;
        if (string.IsNullOrEmpty(name))
        {
            toInsert = dialogue + "\n";
        }
        else
        {
            toInsert = name + ": " + dialogue + "\n";
        }

        //Log += toInsert;

        //Does not work across jumps
        
        if (LogQueue.Count >= CAPACITY)
            LogQueue.Dequeue();
        LogQueue.Enqueue(toInsert);
        
    }

    private void ChangeTextField()
    {
        //_textField.text = Log;
        
        Log = "";
        foreach (string s in LogQueue) { Log += s; }
        _textField.text = Log;
        
    }
}
