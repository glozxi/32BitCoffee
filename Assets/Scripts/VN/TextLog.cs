using UnityEngine;
using TMPro;
using Ink.Runtime;

public class TextLog : MonoBehaviour
{

    [SerializeField]
    private TMP_Text textField;

    // Currently the text log is stored in a string,
    // but might need to change it.
    public string Log
    { get; set; }

    private void Awake()
    {
        DisplayLog();
    }

    public void RecordAndDisplay(TMP_Text name, TMP_Text dialogue)
    {
        RecordText(name.text, dialogue.text);
        DisplayLog();
    }

    public void RecordAndDisplay(Choice choice)
    {
        RecordText("You chose: ", choice.text);
        DisplayLog();
    }

    private void RecordText(string name, string dialogue)
    {
        if (string.IsNullOrEmpty(name))
        {
            Log += dialogue + "\n";
        }
        else
        {
            Log += name + ": " + dialogue + "\n";
        }
    }

    private void DisplayLog()
    {
        textField.text = Log;
    }

}
