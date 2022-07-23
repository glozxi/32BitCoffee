using UnityEngine;
using UnityEngine.UI;

public class StopTimeOnClick : MonoBehaviour
{
    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Time.timeScale = 0;
    }
}
