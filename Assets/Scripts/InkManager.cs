using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Changes dialogue and characters
public class InkManager : MonoBehaviour
{
    private Story story;
    public Story ThisStory
    {
        get => story;
    }

    [SerializeField]
    private TextAsset inkJsonAsset;

    [SerializeField]
    private TMP_Text dialogueTextField;

    [SerializeField]
    private TMP_Text nameTextField;

    [SerializeField]
    private Animator characterAnimator;

    private VerticalLayoutGroup choiceButtonContainer;
    private ChoiceLayoutScript choicesLayoutScript;

    private const string CHARACTER_IMAGE = "character_image";
    private const string SPEAKER_NAME = "speaker_name";

    // Start is called before the first frame update
    void Start()
    {
        choiceButtonContainer = FindObjectOfType<VerticalLayoutGroup>();
        choicesLayoutScript = choiceButtonContainer.GetComponent<ChoiceLayoutScript>();

        StartStory();
    }

    private void StartStory()
    {
        story = new Story(inkJsonAsset.text);
        DisplayNextLine();
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

    public void DisplayChoices()
    {   if (choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0)
        {
            return;
        }

        foreach (Choice choice in story.currentChoices)
        {
            choicesLayoutScript.CreateChoiceButton(choice);

        }
    }

    public void RefreshChoiceView()
    {
        foreach (Button button in choiceButtonContainer.GetComponentsInChildren<Button>())
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