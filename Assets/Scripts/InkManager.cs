using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    private VerticalLayoutGroup choiceButtonContainer;
    private ChoiceLayoutScript choicesLayoutScript;

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
}
