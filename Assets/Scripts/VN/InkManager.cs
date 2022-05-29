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

    private Story story;

    [SerializeField]
    private TextAsset inkJsonAsset;

    [SerializeField]
    private TMP_Text dialogueTextField;

    [SerializeField]
    private TMP_Text nameTextField;

    // [SerializeField]
    // private Animator characterAnimator;

    private Image PosL;
    private Image PosM;
    private Image PosR;

    private Image currentlyOn;

    private VerticalLayoutGroup _choiceButtonContainer;

    //For Tags 
    // Needs to have a better way to do this. If not this means I need to keep track of POS, before i can load IMG, and expression.
    // And animation should be applied to Expression
    private const string POSITION = "Pos"; // INK rule: Always start with POS, IMG, EXPR 
    private const string CHARACTER_IMAGE = "Img";
    private const string CHARACTER_EXPRESSION = "Expression";

    private const string SPEAKER_NAME = "Char";
    private const string BACKGROUND = "Bg";
    private const string SOUND_FX = "Fx";

    // private const string EFFECTS = "SpecialFx"; //Also related to Camera

    //private const 

    // Start is called before the first frame update
    void Awake()
    {
        _choiceButtonContainer = FindObjectOfType<VerticalLayoutGroup>();

        ChoiceButtonScript.Choices += OnChoicePicked;

        StartStory();
    }

    private void OnDisable()
    {
        ChoiceButtonScript.Choices -= OnChoicePicked;
    }

    private void StartStory()
    {
        story = new Story(inkJsonAsset.text);
        DisplayNextLine();
    }

    // Called when a choice button is clicked
    private void OnChoicePicked(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        DisplayNextLine();
        RefreshChoiceView();
    }

    public void DisplayNextLine()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            dialogueTextField.text = text.Trim();
            HandleTags(story.currentTags);
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    private void DisplayChoices()
    {   
            Choices?.Invoke(story.currentChoices);
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

            Debug.Log("TAg is "+ splitTag);

            switch (tagKey)
            {
                case SPEAKER_NAME:
                    if (tagValue == "None")
                    {
                        nameTextField.text = "";
                        nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = false;
                        // nameTextField.GetComponent<Canvas>().enabled = false;
                    } else
                    {
                        nameTextField.transform.parent.gameObject.GetComponent<Image>().enabled = true;
                        nameTextField.text = tagValue;
                    }
                    break;

        
                case CHARACTER_IMAGE:
                    // characterAnimator.Play(tagValue); //Removed maybe used in future idk
                    break;

                case SOUND_FX:
                    // audio.PlayOneShot((AudioClip)Resources.Load("music.mp3"));
                    break;

            }
        }
    }
}
