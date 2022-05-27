using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        FindObjectOfType<InkManager>().DisplayNextLine();
    }
}
