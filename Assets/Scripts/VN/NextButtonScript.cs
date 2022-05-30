using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    public delegate void NextClicked();
    public static event NextClicked Next;

    void Awake()
    {
        Button thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(() => Next?.Invoke());
    }

}
