using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class ChoiceButtonScript : MonoBehaviour
{
    public delegate void ChoicePicked(Choice choice);
    public static event ChoicePicked Choices;

    private Button _button;

    void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        ChoiceLayoutScript.ButtonMade += SetButton;
    }

    private void SetButton(Choice choice)
    {
        TMP_Text buttonText = _button.GetComponentInChildren<TMP_Text>();
        buttonText.text = choice.text;

        _button.onClick.AddListener(() => Choices?.Invoke(choice));

        ChoiceLayoutScript.ButtonMade -= SetButton;
    }


}
