using UnityEngine;
using TMPro;
using Ink.Runtime;
using System.Collections.Generic;

// When new line added to queue, save in Log string
// When new line added to string, save in queue
public class TextLog : MonoBehaviour
{

    [SerializeField]
    private TMP_Text _textField;

    [SerializeField]
    private GameObject _content;

    // Currently the text log is stored in a string,
    // but might need to change it.

    public string Log
    {
        get => QueueToString();
        set
        {
            string[] splitStr = value.TrimEnd('\n').Split('\n');
            foreach (string str in splitStr)
            {
                FixedSizeEnqueue(str + "\n");

            }
        }
    }

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

        //Does not work across jumps
        
        FixedSizeEnqueue(toInsert);
        
    }

    private void ChangeTextField()
    {
        _textField.text = QueueToString();
        
    }

    private string QueueToString()
    {
        string str = "";
        foreach (string s in LogQueue) { str += s; }
        return str;
    }

    private void FixedSizeEnqueue(string toInsert)
    {
        if (LogQueue.Count >= CAPACITY)
            LogQueue.Dequeue();
        LogQueue.Enqueue(toInsert);
    }
}
