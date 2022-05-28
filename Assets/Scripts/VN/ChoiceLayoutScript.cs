using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections.Generic;

public class ChoiceLayoutScript : MonoBehaviour
{
    public delegate void ChoiceButtonMade(Choice choice);
    public static event ChoiceButtonMade ButtonMade;

    private VerticalLayoutGroup choiceButtonContainer;

    [SerializeField]
    private Button choiceButtonPrefab;

    private void Awake()
    {
        choiceButtonContainer = gameObject.GetComponent<VerticalLayoutGroup>();
        InkManager.Choices += OnChoices;
    }

    private void OnDisable()
    {
        InkManager.Choices -= OnChoices;
    }

    private void OnChoices(List<Choice> choices)
    {
        // Choices already displayed
        if (choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0)
        {
            return;
        }

        foreach (Choice choice in choices)
        {
            CreateChoiceButton(choice);
        }
    }

    // Creates new choice button in layout
    public void CreateChoiceButton(Choice choice)
    {
        Button button = Instantiate(choiceButtonPrefab);
        button.transform.SetParent(choiceButtonContainer.transform);

        ButtonMade?.Invoke(choice);
    }
}
