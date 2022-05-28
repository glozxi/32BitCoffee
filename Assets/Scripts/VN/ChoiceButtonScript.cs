using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class ChoiceButtonScript : MonoBehaviour
{
    public delegate void ChoicePicked(Choice choice);
    public static event ChoicePicked Choices;

    private Button button;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        ChoiceLayoutScript.ButtonMade += SetButton;
    }

    private void SetButton(Choice choice)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = choice.text;

        button.onClick.AddListener(() => Choices?.Invoke(choice));

        ChoiceLayoutScript.ButtonMade -= SetButton;
    }


}
