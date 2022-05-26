using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class ChoiceLayoutScript : MonoBehaviour
{
    private VerticalLayoutGroup choiceButtonContainer;

    [SerializeField]
    private Button choiceButtonPrefab;

    private void Start()
    {
        choiceButtonContainer = gameObject.GetComponent<VerticalLayoutGroup>();
    }

    // Creates new choice button in layout
    public Button CreateChoiceButton(Choice choice)
    {
        Button button = Instantiate(choiceButtonPrefab);
        button.transform.SetParent(choiceButtonContainer.transform);

        // get the button to contain a choice
        button.GetComponent<ChoiceButtonScript>().ThisChoice = choice;

        return button;
    }
}
