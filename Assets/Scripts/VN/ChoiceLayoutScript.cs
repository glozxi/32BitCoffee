using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class ChoiceLayoutScript : MonoBehaviour
{
    private VerticalLayoutGroup choiceButtonContainer;
    private InkManager inkManager;

    [SerializeField]
    private Button choiceButtonPrefab;

    private void Awake()
    {
        inkManager = FindObjectOfType<InkManager>();
        choiceButtonContainer = gameObject.GetComponent<VerticalLayoutGroup>();
    }

    // Creates new choice button in layout
    public Button CreateChoiceButton(Choice choice)
    {
        Button button = Instantiate(choiceButtonPrefab);
        button.transform.SetParent(choiceButtonContainer.transform);

        ChoiceButtonScript buttonScript = button.GetComponent<ChoiceButtonScript>();
        buttonScript.SetTask(inkManager, choice);

        return button;
    }
}
