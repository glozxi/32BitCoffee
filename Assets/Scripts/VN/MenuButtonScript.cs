using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        menu.SetActive(!menu.activeSelf);
        GetComponentInChildren<TMP_Text>().text = menu.activeSelf ? "Back" : "Menu";
    }
}
