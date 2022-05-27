using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class ChoiceButtonScript : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void SetTask(InkManager inkManager, Choice choice)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        buttonText.text = choice.text;

        button.onClick.AddListener(() => 
        {
            inkManager.ThisStory.ChooseChoiceIndex(choice.index);
            inkManager.DisplayNextLine();
            inkManager.RefreshChoiceView();
        });
            
    }

}
