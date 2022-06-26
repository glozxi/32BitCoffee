using UnityEngine;
using UnityEngine.UI;

public class ExitMenuButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

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
        if (_menu.activeSelf)
        {
            _menu.SetActive(false);
        }
    }
}
