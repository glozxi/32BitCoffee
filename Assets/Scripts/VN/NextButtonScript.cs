using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    // TODO
    // Similar to ChoiceButtonScript, make a delegate and event. From Game Manager, add a
    // method that to displays next line to the event.

    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    // TODO
    // In this method, invoke the event. 
    private void TaskOnClick()
    {
        FindObjectOfType<InkManager>().DisplayNextLine();
    }
}
