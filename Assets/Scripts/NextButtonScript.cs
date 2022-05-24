using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    private InkManager inkManager;
    private Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        inkManager = FindObjectOfType<InkManager>();
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        inkManager.DisplayNextLine();
    }
}
