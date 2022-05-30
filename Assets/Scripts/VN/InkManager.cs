using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Changes dialogue and characters
public class InkManager : MonoBehaviour
{
    public delegate void ChoicesEncountered(List<Choice> choices);
    public static event ChoicesEncountered Choices;

    private Story _story;

    [SerializeField]
    private TextAsset _inkJsonAsset;

    [SerializeField]
    private TMP_Text _dialogueTextField;

    [SerializeField]
    private TMP_Text _nameTextField;

    // [SerializeField]
    // private Animator characterAnimator;

    // TO USE IN FUTURE
    /*
    private Image PosL;
    private Image PosM;
    private Image PosR;

    private Image currentlyOn;
    */

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
        _choiceButtonContainer = FindObjectOfType<VerticalLayoutGroup>();

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
        DisplayNextLine();
    }

    // Called when a choice button is clicked
    private void OnChoicePicked(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index);
        DisplayNextLine();
        RefreshChoiceView();
    }

    // Called when next button clicked
    private void OnNext()
    {
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (_story.canContinue)
        {
            string text = _story.Continue();
            _dialogueTextField.text = text.Trim();
            HandleTags(_story.currentTags);
        }
        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    private void DisplayChoices()
    {   
            Choices?.Invoke(_story.currentChoices);
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

            // Debug.Log("Tag is "+ splitTag);

            switch (tagKey)
            {
                case SpeakerName:
                    if (tagValue == "None")
                    {
                        _nameTextField.text = "";
                        _nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = false;
                        // nameTextField.GetComponent<Canvas>().enabled = false;
                    } else
                    {
                        _nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = true;
                        _nameTextField.text = tagValue;
                    }
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
}
