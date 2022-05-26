using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class ChoiceButtonScript : MonoBehaviour
{
    private Story story;
    private InkManager inkManager;
    private Choice thisChoice;
    public Choice ThisChoice
    {
        set => thisChoice = value;
    }

    private void Start()
    {
        inkManager = FindObjectOfType<InkManager>();
        story = inkManager.ThisStory;
        Button thisButton = gameObject.GetComponent<Button>();

        thisButton.onClick.AddListener(TaskOnClick);

        TMP_Text buttonText = thisButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = thisChoice.text;
    }

    private void TaskOnClick()
    {
        story.ChooseChoiceIndex(thisChoice.index);
        inkManager.DisplayNextLine();
        inkManager.RefreshChoiceView();
    }
}
