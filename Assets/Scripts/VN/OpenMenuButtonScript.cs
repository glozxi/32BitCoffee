using UnityEngine;
using UnityEngine.UI;

public class OpenMenuButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject _menu;

    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (!_menu.activeSelf)
        {
            _menu.SetActive(true);
        }
    }
}