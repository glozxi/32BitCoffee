using UnityEngine;
using TMPro;
using Ink.Runtime;

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
        if (string.IsNullOrEmpty(name))
        {
            Log += dialogue + "\n";
        }
        else
        {
            Log += name + ": " + dialogue + "\n";
        }
    }

    private void ChangeTextField()
    {
        _textField.text = Log;
    }

    public string GetTextLog()
    {
        return Log;
    }

    public void SetTextLog(string log)
    {
        Log = log;
    }
}
