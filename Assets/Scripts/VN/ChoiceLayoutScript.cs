using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections.Generic;

public class ChoiceLayoutScript : MonoBehaviour
{
    public delegate void ChoiceButtonMade(Choice choice);
    public static event ChoiceButtonMade ButtonMade;

    private VerticalLayoutGroup _choiceButtonContainer;

    [SerializeField]
    private Button _choiceButtonPrefab;

    private void Awake()
    {
        _choiceButtonContainer = gameObject.GetComponent<VerticalLayoutGroup>();
        InkManager.Choices += OnChoices;
    }

    private void OnDisable()
    {
        InkManager.Choices -= OnChoices;
    }

    private void OnChoices(List<Choice> choices)
    {
        // Choices already displayed
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0)
        {
            return;
        }

        foreach (Choice choice in choices)
        {
            CreateChoiceButton(choice);
        }
    }

    // Creates new choice button in layout
    private void CreateChoiceButton(Choice choice)
    {
        Button button = Instantiate(_choiceButtonPrefab);
        button.transform.SetParent(_choiceButtonContainer.transform);

        ButtonMade?.Invoke(choice);
    }
}
