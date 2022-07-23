using UnityEngine.UI;
using UnityEngine;

public class ResumeTimeOnClick : MonoBehaviour
{
    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        Time.timeScale = 1;
    }
}
