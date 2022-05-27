using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ink.Runtime;

public class ChoiceLayoutScript : MonoBehaviour
{
    private VerticalLayoutGroup choiceButtonContainer;

    [SerializeField]
    private Button choiceButtonPrefab;

    private void Awake()
    {
        choiceButtonContainer = gameObject.GetComponent<VerticalLayoutGroup>();
    }

    // Creates new choice button in layout
    public void CreateChoiceButton(UnityAction task, Choice choice)
    {
        Button button = Instantiate(choiceButtonPrefab);
        button.transform.SetParent(choiceButtonContainer.transform);

        ChoiceButtonScript buttonScript = button.GetComponent<ChoiceButtonScript>();
        buttonScript.SetText(choice.text);
        buttonScript.SetTask(task);
    }
}
