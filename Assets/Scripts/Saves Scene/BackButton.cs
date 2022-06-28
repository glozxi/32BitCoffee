using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick());
    }

    private void OnClick()
    {
        PreviousSceneChanger.Instance.LoadPreviousScene();
    }
}
