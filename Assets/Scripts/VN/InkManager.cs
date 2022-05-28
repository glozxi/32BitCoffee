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

    [SerializeField]
    private Animator characterAnimator;

    private VerticalLayoutGroup _choiceButtonContainer;

    private const string CHARACTER_IMAGE = "character_image";
    private const string SPEAKER_NAME = "speaker_name";

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

            switch (tagKey)
            {
                case SPEAKER_NAME:
                    nameTextField.text = tagValue;
                    break;
                case CHARACTER_IMAGE:
                    characterAnimator.Play(tagValue);
                    break;

            }
        }
    }
}
