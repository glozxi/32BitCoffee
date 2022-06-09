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
    private string _loadedState;

    [SerializeField]
    private TextAsset _inkJsonAsset;

    [SerializeField]
    private TMP_Text _dialogueTextField;

    [SerializeField]
    private TMP_Text _nameTextField;

    [SerializeField]
    private TextLog _textLog;

    // [SerializeField]
    // private Animator characterAnimator;

    // TO USE IN FUTURE
    /*
    private Image PosL;
    private Image PosM;
    private Image PosR;

    private Image currentlyOn;
    */

    [SerializeField]
    private VerticalLayoutGroup _choiceButtonContainer;

    //For Tags 
    // Needs to have a better way to do this. If not this means I need to keep track of POS, before i can load IMG, and expression.
    // And animation should be applied to Expression
    // TO USE IN FUTURE
    /*
    private const string Position = "Pos"; // INK rule: Always start with POS, IMG, EXPR 
    private const string CharacterImage = "Img";
    private const string CharacterExpression = "Expression";

    private const string Background = "Bg";
    private const string SoundFx = "Fx";
    */

    private const string SpeakerName = "Char";

    // private const string EFFECTS = "SpecialFx"; //Also related to Camera

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
        _story = new Story(_inkJsonAsset.text);

        // Loaded story
        if (!string.IsNullOrEmpty(_loadedState))
        {
            _story?.state?.LoadJson(_loadedState);
            DisplayThisLine();
        }
        // New story
        else
        {
            DisplayNextLine();
        }

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
        foreach (string tag in currentTags)
        {
            
            string[] splitTag = tag.Split(':');
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SpeakerName:
                    SetName(tagValue);
                    break;

        
                // TO USE IN FUTURE
                /*
                case CharacterImage:
                    // characterAnimator.Play(tagValue); //Removed maybe used in future idk
                    break;

                case SoundFx:
                    // audio.PlayOneShot((AudioClip)Resources.Load("music.mp3"));
                    break;
                */

            }
        }
    }

    private void SetName(string name)
    {
        if (name == "None")
        {
            _nameTextField.text = "";
            _nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = false;
            // nameTextField.GetComponent<Canvas>().enabled = false;
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

    public string GetStoryState()
    {
        return _story.state.ToJson();
    }

    public void LoadState(string inkStoryState, string textLog)
    {
        _loadedState = inkStoryState;
        _textLog.SetTextLog(textLog);

        StartStory();
    }

    public string GetTextLog()
    {
        return _textLog.GetTextLog();
    }
}
