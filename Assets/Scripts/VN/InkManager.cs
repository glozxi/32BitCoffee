using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Changes dialogue and characters
// TODO: Split into smaller classes
public class InkManager : MonoBehaviour
{
    public delegate void ChoicesEncountered(List<Choice> choices);
    public static event ChoicesEncountered Choices;

    private Story _story;
    private static string _loadedState;
    private static string _textInLog;

    // Prevents brew repeating after returning to VN
    private static bool _loadedFromBrew = false;

    // Determines if brew or not on next button pressed
    private bool _isBrewNext = false;
    // Name of next brew level
    private string _nextLevel;
    private State _state;

    [SerializeField]
    private TextAsset _inkJsonAsset;

    [SerializeField]
    private TMP_Text _dialogueTextField;

    [SerializeField]
    private TMP_Text _nameTextField;

    [SerializeField]
    private TextLog _textLog;

    [SerializeField]
    private VerticalLayoutGroup _choiceButtonContainer;

    // private const string SPEAKERNAME = "SPEAKER";
    private const string SPEAKERNAME = "Char";
    private const string MODEL = "MODEL";
    private const string BACKGROUND = "BG";
    private const string BGM = "BGM";
    private const string FX = "FX";
    private const string PRELOAD = "PRELOAD";
    private const string TOBREW = "TOBREW";

    void Awake()
    {
        ChoiceButtonScript.Choices += OnChoicePicked;
        NextButtonScript.Next += OnNext;
        
        StartStory();
    }

    private void OnDisable()
    {
        ChoiceButtonScript.Choices -= OnChoicePicked;
        NextButtonScript.Next -= OnNext;
    }

    private void StartStory()
    {
        _state = FindObjectOfType<State>();
        _story = new Story(_inkJsonAsset.text);

        ObserveVariables();

        // Loaded story
        if (!string.IsNullOrEmpty(_loadedState))
        {
            _story?.state?.LoadJson(_loadedState);
            SetInkVariables();
            _textLog.Log = _textInLog;
            DisplayThisLine();
        }
        // New story
        else
        {
            DisplayNextLine();
        }

    }

    private void ObserveVariables()
    {
        _story.ObserveVariable("Drink", (string varName, object newValue) => {
            _state.Drink = (string)newValue;
        });
    }

    // Called when a choice button is clicked
    private void OnChoicePicked(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        RecordChoiceInLog(choice);
        DisplayNextLine();
        RefreshChoiceView();
    }

    // Called when next button clicked
    private void OnNext()
    {
        if (_isBrewNext)
        {
            TransitToBrew(_nextLevel);
            return;
        }
        RecordLineInLog();
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            _story.Continue();
            DisplayThisLine();
        }
        else
        {
            HandleChoices();
        }
    }

    private void DisplayThisLine()
    {
        string text = _story.currentText.Trim();

        _dialogueTextField.text = text;
        HandleTags(_story.currentTags);
        HandleChoices();
    }

    private void HandleChoices()
    {
        if (_story.currentChoices.Count > 0)
        {
            Choices?.Invoke(_story.currentChoices);
        }
    }

    private void RefreshChoiceView()
    {
        foreach (Button button in _choiceButtonContainer.GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        //where to retrieve
        float volume = 1f;
        float pitch = 1f;

        foreach (string tag in currentTags)
        {
            
            string[] splitTag = tag.Split(':');
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKERNAME:
                    SetName(tagValue);
                    break;

                case PRELOAD:
                    Debug.Log("preloading.."+ tagValue);
                    CharacterManager.instance.CMPreLoadChar(tagValue);
                    break;

                case MODEL:
                    string[] reqData = tagValue.Split(",");
                    //Resolve in Google Sheets
                    //CHARNAME,POSE,EXPR,POSITION
                    CharacterManager.instance.CMChar(reqData[0], reqData[1], reqData[2], reqData[3]);
                    break;

                case FX:
                   AudioManager.instance.PlaySFX(tagValue, volume, pitch);
                    break;

                case BGM:
                    float startingVolume = 0.8f;
                    bool playOnStart = true;
                    bool loop = true;
                    AudioManager.instance.PlayBGM(tagValue, volume, pitch, startingVolume,playOnStart,loop);
                    break;

                case BACKGROUND:
                    BackgroundManager.instance.CMChangeBackground(tagValue);
                    break;

                case TOBREW:
                    if (_loadedFromBrew)
                    {
                        _loadedFromBrew = false;
                        _isBrewNext = false;
                        OnNext();
                        break;
                    }
                    _isBrewNext = true;
                    _nextLevel = tagValue;
                    break;
            }
        }
    }
    

    private void SetName(string name)
    {
        if (name == "None")
        {
            _nameTextField.text = "";
            _nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            _nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = true;
            _nameTextField.text = name;
        }
    }

    private void RecordLineInLog()
    {
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length == 0)
        {
            _textLog.RecordAndChangeTextField(_nameTextField, _dialogueTextField);
        }
    }

    private void RecordChoiceInLog(Choice choice)
    {
        _textLog.RecordAndChangeTextField(choice);
    }

    private void TransitToBrew(string level)
    {
        _state.NextBrewLevel = level;
        _state.InkStoryState = GetStoryState();
        _state.TextLog = GetTextLog();

        UnityEngine.SceneManagement.SceneManager.LoadScene("BrewScene");
    }

    public void SaveBeforeLoadScene()
    {
        _state.InkStoryState = GetStoryState();
        _state.TextLog = GetTextLog();
    }

    private void SetInkVariables()
    {
        SetInkVariable("Outcome", _state.Outcome);
        SetInkVariable("Drink", _state.Drink);
    }

    private void SetInkVariable(string varName, object toAssign)
    {
        if (toAssign != null)
        {
            _story.variablesState[varName] = toAssign;
        }
    }

    public string GetStoryState()
    {
        return _story.state.ToJson();
    }

    public static void LoadState(string inkStoryState, string textLog)
    {
        _loadedState = inkStoryState;
        _textInLog = textLog;
        _loadedFromBrew = false;
    }

    public void LoadStateFromBrew(string inkStoryState, string textLog)
    {
        _loadedState = inkStoryState;
        _textInLog = textLog;
        _loadedFromBrew = true;
        StartStory();
    }

    public static void ResetStory()
    {
        _loadedState = null;
        _textInLog = null;
        _loadedFromBrew = false;
    }

    public string GetTextLog()
    {
        return _textLog.Log;
    }
}
