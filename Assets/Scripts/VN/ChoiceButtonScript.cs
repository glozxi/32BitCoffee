using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ChoiceButtonScript : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void SetText(string text)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = text;
    }

    public void SetTask(UnityAction task)
    {
        button.onClick.AddListener(task);
    }

}
