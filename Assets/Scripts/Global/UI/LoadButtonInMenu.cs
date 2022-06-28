using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadButtonInMenu : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick());
    }
    private void OnClick()
    {
        SceneManager.LoadScene("SaveSelect");
    }
}
